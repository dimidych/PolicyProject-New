using System.Threading.Tasks;

namespace RabbitMqEventBus
{
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    public interface IIntegrationEventHandler
    {
    }

    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
