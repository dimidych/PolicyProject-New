using EventBus;

namespace DeviceService.Integration.Events
{
    public class DeviceUpdatedEvent : IntegrationEvent
    {
        public DeviceUpdatedEvent(Device device)
        {
            Device = device;
        }

        public Device Device { get; }
    }
}