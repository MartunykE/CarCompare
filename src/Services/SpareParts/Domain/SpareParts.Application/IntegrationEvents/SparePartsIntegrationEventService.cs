using EventBus.Abstractions;
using EventBus.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Application.IntegrationEvents
{
    public class SparePartsIntegrationEventService: ISparePartsIntegrationEventService
    {
        private readonly IEventBus eventBus;
        public SparePartsIntegrationEventService(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            eventBus.Publish(@event);
            return Task.CompletedTask;
        }

        public Task SaveEventAndCatalogContextChangesAsync(IntegrationEvent evt)
        {
            throw new NotImplementedException();
        }
    }
}
