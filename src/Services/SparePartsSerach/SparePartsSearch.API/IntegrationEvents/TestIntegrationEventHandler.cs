using EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.IntegrationEvents
{
    public class TestIntegrationEventHandler : IIntegrationEventHandler<TestIntegrationEvent>
    {
        public Task Handle(TestIntegrationEvent @event)
        {
            Debug.WriteLine($"{@event.Message.Name} : MESSAGE");
            return Task.CompletedTask;
        }
    }
}
