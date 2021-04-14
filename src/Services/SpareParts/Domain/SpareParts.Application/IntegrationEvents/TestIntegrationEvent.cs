using EventBus.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.IntegrationEvents
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
