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
        public IEnumerable<SparePart> SpareParts { get; }
        public AskForSparePartsPricesIntegrationEvent(IEnumerable<SparePart> spareParts)
        {
            SpareParts = spareParts;
        }
    }
}
