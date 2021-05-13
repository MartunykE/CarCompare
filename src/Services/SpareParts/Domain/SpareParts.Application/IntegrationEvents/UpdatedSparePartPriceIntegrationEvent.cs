using EventBus.Abstractions.Events;
using SpareParts.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.IntegrationEvents
{
    class UpdatedSparePartPriceIntegrationEvent: IntegrationEvent
    {
        public UpdatedSparePartPriceIntegrationEvent(SparePartDTO sparePartDTO)
        {

        }
    }
}
