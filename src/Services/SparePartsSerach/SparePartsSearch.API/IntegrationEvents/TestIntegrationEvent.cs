using EventBus.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SparePartsSearch.API.IntegrationEvents
{
    public class TestIntegrationEvent: IntegrationEvent
    {
        public string Message { get; set; }

        public TestIntegrationEvent(string message)
        {
            Message = message;
        }
    }
}
