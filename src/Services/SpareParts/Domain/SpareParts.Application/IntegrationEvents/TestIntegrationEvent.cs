using EventBus.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.Application.IntegrationEvents
{
    public class A
    {
        public string Name { get; set; }
        public int Year { get; set; }
    }
    public class TestIntegrationEvent: IntegrationEvent
    {
        public A Message { get; set; }

        public TestIntegrationEvent(A message)
        {
            Message = message;
        }
    }
}
