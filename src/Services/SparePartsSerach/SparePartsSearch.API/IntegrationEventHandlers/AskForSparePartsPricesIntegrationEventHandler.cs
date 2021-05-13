using EventBus.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SparePartsSearch.API.IntegrationEvents;
namespace SparePartsSearch.API.IntegrationEventHandlers
{
    public class AskForSparePartsPricesIntegrationEventHandler : IIntegrationEventHandler<AskForSparePartsPricesIntegrationEvent>
    {
        public Task Handle(AskForSparePartsPricesIntegrationEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
