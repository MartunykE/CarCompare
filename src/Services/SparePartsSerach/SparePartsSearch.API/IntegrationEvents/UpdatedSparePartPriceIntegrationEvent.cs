using EventBus.Abstractions.Events;
using SparePartsSearch.API.Models;

namespace SparePartsSearch.API.IntegrationEvents
{
    class UpdatedSparePartPriceIntegrationEvent: IntegrationEvent
    {
        public UpdatedSparePartPriceIntegrationEvent(SparePart sparePart)
        {

        }
    }
}
