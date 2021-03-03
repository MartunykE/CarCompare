using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Abstractions.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
