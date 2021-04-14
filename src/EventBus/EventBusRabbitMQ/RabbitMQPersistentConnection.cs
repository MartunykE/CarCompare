using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.IO;
using System.Net.Sockets;

namespace EventBusRabbitMQ
{
    public class RabbitMQPersistentConnection : IRabbitMQPersistentConnection
    {
        private readonly IConnectionFactory connectionFactory;
        private readonly ILogger<RabbitMQPersistentConnection> logger;
        private readonly int retryCount;

        IConnection connection;
        bool disposed;

        public RabbitMQPersistentConnection(IConnectionFactory connectionFactory, ILogger<RabbitMQPersistentConnection> logger, int retryCount = 5)
        {
            this.connectionFactory = connectionFactory;
            this.logger = logger;
            this.retryCount = retryCount;
        }

        public bool IsConnected
        {
            get
            {
                return connection != null && connection.IsOpen && !disposed;
            }
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connection");
            }

            return connection.CreateModel();
        }

        public void Dispose()
        {
            if (disposed) return;

            disposed = true;
            try
            {
                connection.Dispose();
            }
            catch (IOException ex)
            {
                logger.LogCritical(ex.Message);
            }
        }

        public bool TryConnect()
        {
            logger.LogInformation("Rabbit MQ is trying to connect");

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(retryCount, retryAttempt => TimeSpan.FromSeconds(20), (ex, time) =>

                {
                    logger.LogWarning(ex, $"Rabbit couldn`t connect after {time.TotalSeconds} {ex.Message}");
                }
            );

            policy.Execute(() =>
            {
                connection = connectionFactory.CreateConnection();
            });

            if (IsConnected)
            {
                connection.ConnectionShutdown += OnConnectionShutDown;
                connection.CallbackException += OnCallbackException;
                connection.ConnectionBlocked += OnConectionBlocked;

                logger.LogInformation($"RabbitMQ persistent connections to {connection.Endpoint.HostName}");
                return true;
            }

            logger.LogCritical("Can`t create/open RabbitMQ connection");
            return false;
        }

        private void OnConnectionShutDown(object sender, ShutdownEventArgs args)
        {
            if (disposed) return;
            
            logger.LogWarning("RabbitMQ connection is on shutdown. Trying to reconnect");
            TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs args)
        {
            if (disposed) return;

            logger.LogWarning("RabbitMQ connection thown an exception. Trying to reconnect");
            TryConnect();
        }

        private void OnConectionBlocked(object sender, ConnectionBlockedEventArgs args)
        {
            if (disposed) return;

            logger.LogWarning("RabbitMQ connection is blocked. Trying to reconnect");
            TryConnect();
        }

    }
}
