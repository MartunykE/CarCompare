using EventBus.Abstractions.Events;
using SpareParts.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.IntegrationEvents
{
    public class AskForSparePartsPricesIntegrationEvent: IntegrationEvent
    {      
        public VehicleDTO Vehicle { get; private set; }
        public string Msg => "Hi";
        public AskForSparePartsPricesIntegrationEvent(VehicleDTO vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
