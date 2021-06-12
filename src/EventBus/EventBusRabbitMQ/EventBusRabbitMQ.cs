using EventBus.Abstractions;
using EventBus.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using Microsoft.Extensions.Logging;
using Polly.Retry;
using System.Net.Sockets;
using Polly;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Autofac;

namespace EventBusRabbitMQ
{
    public class EventBusRabbitMQ : IEventBus, IDisposable
    {
        const string BROKER_NAME = "CarCompareBus";

        private readonly IRabbitMQPersistentConnection persistentConnection;
        private readonly IEventBusSubscriptionManager subscriptionManager;
        private readonly ILogger<EventBusRabbitMQ> logger;
        private readonly ILifetimeScope autofac;
        private readonly int retryCount;
        
        private IModel consumerChannel;
        private string queueName;

        public EventBusRabbitMQ(IRabbitMQPersistentConnection persistentConnection, ILogger<EventBusRabbitMQ> logger,
            IEventBusSubscriptionManager subscriptionManager, ILifetimeScope autofac, string queueName = null, int retryCount = 4)
        {
            this.persistentConnection = persistentConnection;
            this.subscriptionManager = subscriptionManager;
            this.logger = logger;
            this.retryCount = retryCount;
            this.queueName = queueName;
            this.autofac = autofac;
            consumerChannel = CreateConsumerChannel();
            subscriptionManager.OnEventRemoved += OnEventRemoved;
        }

        public void Publish(IntegrationEvent @event)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var retryPolicy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(retryCount, nextRetry => TimeSpan.FromSeconds(30), (ex, time) =>
                {
                    logger.LogWarning(ex, $"Cannot publish event: {@event.Id} Error: {ex.Message} ");
                });

            var eventName = @event.GetType().Name;

            logger.LogTrace($"Creating RabbitMQ channel to publish event {@event.Id} {eventName}");

            using (var channel = persistentConnection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: BROKER_NAME, type: ExchangeType.Fanout);
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                retryPolicy.Execute(() =>
                {
                    var basicProperties = channel.CreateBasicProperties();
                    basicProperties.DeliveryMode = 2;

                    logger.LogTrace($"Publishing event to RabbitMQ {@event.Id}");

                    channel.BasicPublish(
                        exchange: BROKER_NAME,
                        routingKey: eventName,
                        mandatory: true,
                        basicProperties: basicProperties,
                        body: body);
                });
            }
        }

        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = subscriptionManager.GetEventKey<T>();
            DoInternalSubscription(eventName);

            logger.LogTrace($"Subscribing for event {eventName}");

            subscriptionManager.AddSubscription<T, TH>();
            StartBasicConsume();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = subscriptionManager.GetEventKey<T>();
            
            logger.LogInformation($"Unsubscribing from event {eventName}");

            subscriptionManager.RemoveSubscription<T, TH>();
        }

        public void Dispose()
        {
            if (consumerChannel != null)
            {
                consumerChannel.Dispose(); 
            }

            subscriptionManager.Clear();
        }

        private IModel CreateConsumerChannel()
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            logger.LogTrace("Creating RabbitmMQ channel");

            var channel = persistentConnection.CreateModel();

            channel.ExchangeDeclare(exchange: BROKER_NAME, type: ExchangeType.Fanout);

            channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            channel.CallbackException += (sender, ex) =>
            {
                logger.LogWarning(ex.Exception, "Recreatring RabbitMQ channel");
                consumerChannel.Dispose();
                consumerChannel = CreateConsumerChannel();
                StartBasicConsume();
            };

            return channel;
        }

        private void StartBasicConsume()
        {
            logger.LogTrace("Starting RabbitMQ basic consume");

            if (consumerChannel == null)
            {
                logger.LogError("Can`t call StartBasicConsume on consumerChannel == null");
                return;
            }

            var consumer = new AsyncEventingBasicConsumer(consumerChannel);
            consumer.Received += ConsumerReceivedHandler;

            consumerChannel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

        }

        private async Task ConsumerReceivedHandler(object sender, BasicDeliverEventArgs eventArgs)
        {
            var eventName = eventArgs.RoutingKey;
            var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
            try
            {
                if (message.ToLowerInvariant().Contains("throw-fake-exception"))
                {
                    throw new InvalidOperationException($"Fake exception requested {message}");
                }

                await ProcessEvent(eventName, message);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, $"Error Processing message {message}");
            }

            // TODO: Use Dead Letter Exchange
            consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            logger.LogTrace($"Processing RabbitMQ event: {eventName}");

            if (subscriptionManager.HasSubscritptionsForEvent(eventName))
            {
                using (var scope = autofac.BeginLifetimeScope())
                {
                    var subscriptions = subscriptionManager.GetHandlersForEvent(eventName);
                    foreach (var subscription in subscriptions)
                    {
                        var handler = scope.ResolveOptional(subscription.HandlerType);
                        if (handler == null) continue;
                        var eventType = subscriptionManager.GetEventTypeByName(eventName);
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var concreteHandlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                        // TODO: ????
                        await Task.Yield();

                        await (Task)concreteHandlerType.GetMethod("Handle")
                            .Invoke(handler, new object[] { integrationEvent });
                    }
                }

            }
            else
            {
                logger.LogWarning($"No subscription for event : {eventName}");
            }

        }
        private void DoInternalSubscription(string eventName)
        {
            var containsKey = subscriptionManager.HasSubscritptionsForEvent(eventName);

            if (!containsKey)
            {
                if (!persistentConnection.IsConnected)
                {
                    persistentConnection.TryConnect();
                }

                using (var channel = persistentConnection.CreateModel())
                {
                    channel.QueueBind(queue: queueName, exchange: BROKER_NAME, routingKey: eventName);
                }
            }
        }

        private void OnEventRemoved(object sender, string eventName)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            using (var channel = persistentConnection.CreateModel())
            {
                channel.QueueUnbind(queue: queueName, exchange: BROKER_NAME, routingKey: eventName);

                if (subscriptionManager.isEmpty)
                {
                    queueName = string.Empty;
                    consumerChannel.Close();
                }
            }
        }
    }
}
