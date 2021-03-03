  using EventBus.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Abstractions
{
    public interface IEventBusSubscriptionManager
    {
        bool isEmpty { get; }
        event EventHandler<string> OnEventRemoved;
        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
        bool HasSubscritptionsForEvent<T>() where T : IntegrationEvent;
        bool HasSubscritptionsForEvent(string eventName);
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) ;
        string GetEventKey<T>();
        Type GetEventTypeByName(string eventName);
        void Clear();

    }
}
