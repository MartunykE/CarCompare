using EventBus.Abstractions;
using EventBus.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EventBusRabbitMQ
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> handlers;
        private readonly List<Type> eventTypes;

        public event EventHandler<string> OnEventRemoved;

        public InMemoryEventBusSubscriptionManager()
        {
            handlers = new Dictionary<string, List<SubscriptionInfo>>();
            eventTypes = new List<Type>();
        }
        public bool isEmpty => throw new NotImplementedException();

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();

            if (!HasSubscritptionsForEvent(eventName))
            {
                handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            var handlerType = typeof(TH);

            if (handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException($"Handler type: {handlerType.Name} already registred for {eventName}");
            }

            handlers[eventName].Add(SubscriptionInfo.CreateTyped(handlerType));

            if (!eventTypes.Contains(typeof(T)))
            {
                eventTypes.Add(typeof(T));
            }

        }

        public void Clear()
        {
            handlers.Clear();
        }

        public string GetEventKey<T>() => typeof(T).Name;

        public Type GetEventTypeByName(string eventName)
        {
            return eventTypes.SingleOrDefault(t => t.Name == eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => handlers[eventName];


        public bool HasSubscritptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscritptionsForEvent(key);
        }

        public bool HasSubscritptionsForEvent(string eventName) => handlers.ContainsKey(eventName);

        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var subscriptionToRemove = FindSubscription<T, TH>();
            var eventName = GetEventKey<T>();

            if (subscriptionToRemove == null) return;
            handlers[eventName].Remove(subscriptionToRemove);

            if (!handlers[eventName].Any())
            {
                handlers.Remove(eventName);
                var eventType = eventTypes.SingleOrDefault(e => e.Name == eventName);
                if (eventType != null)
                {
                    eventTypes.Remove(eventType);
                }

                RaiseOnEventRemoved(eventName);

            }
            throw new NotImplementedException();
        }
        private void RaiseOnEventRemoved(string eventName)
        {
            //TODO ::???

            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }


        private SubscriptionInfo FindSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            if (!HasSubscritptionsForEvent(eventName))
            {
                return null;
            }

            var handlerType = typeof(TH);

            return handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);
        }
}
}
