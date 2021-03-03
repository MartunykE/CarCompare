using EventBus.Abstractions.Events;
using System.Threading.Tasks;

namespace EventBus.Abstractions
{
    public interface IIntegrationEventHandler <T> where T: IntegrationEvent
    {
        Task Handle(T @event);
    }
}
