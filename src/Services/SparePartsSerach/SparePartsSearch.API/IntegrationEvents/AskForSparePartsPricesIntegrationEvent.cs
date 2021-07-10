using EventBus.Abstractions.Events;
using SparePartsSearch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.IntegrationEvents
{
    public class AskForSparePartsPricesIntegrationEvent : IntegrationEvent
    {
        public Vehicle Vehicle { get; set; }
        public AskForSparePartsPricesIntegrationEvent(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
