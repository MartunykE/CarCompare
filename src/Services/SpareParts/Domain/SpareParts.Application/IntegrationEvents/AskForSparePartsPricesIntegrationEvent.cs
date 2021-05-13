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
        IEnumerable<SparePartDTO> spareParts;
        public AskForSparePartsPricesIntegrationEvent(IEnumerable<SparePartDTO> spareParts)
        {
            this.spareParts = spareParts;
        }
    }
}
