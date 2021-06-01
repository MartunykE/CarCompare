using EventBus.Abstractions.Events;
using SpareParts.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.IntegrationEvents
{
    public class AskForSparePartsPricesIntegrationEvent: IntegrationEvent
    {
        //TODO: vehicle Id
        string vehicleId;
        IEnumerable<SparePartDTO> spareParts;
        public AskForSparePartsPricesIntegrationEvent(string vehicleId, IEnumerable<SparePartDTO> spareParts)
        {
            this.vehicleId = vehicleId;
            this.spareParts = spareParts;
        }
    }
}
