using EventBus.Abstractions;
using EventBus.Abstractions.Events;
using MongoDB.Driver;
using Serilog;
using SpareParts.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Application.IntegrationEvents.Services
{
    public class SparePartsIntegrationEventService: ISparePartsIntegrationEventService
    {
        private readonly IEventBus eventBus;
        private readonly ISparePartsDbContext sparePartsDbContext;
        private readonly IClientSessionHandle clientSessionHandle;
        private readonly ILogger logger;
        public SparePartsIntegrationEventService(
            IEventBus eventBus, ISparePartsDbContext sparePartsDbContext, IClientSessionHandle clientSessionHandle, ILogger logger)
        {
            this.eventBus = eventBus;
            this.sparePartsDbContext = sparePartsDbContext;
            this.clientSessionHandle = clientSessionHandle;
            this.logger = logger;
        }

        public Task PublishThroughEventBusAsync(IntegrationEvent @event)
        {
            //TODO: Add eventLogService
            eventBus.Publish(@event);
            return Task.CompletedTask;
        }
     
    }
}
