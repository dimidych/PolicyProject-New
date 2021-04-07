using EventBus;

namespace DeviceService.Integration.Events
{
    public class DeviceAddedEvent : IntegrationEvent
    {
        public DeviceAddedEvent(Device newDevice)
        {
            NewDevice = newDevice;
        }

        public Device NewDevice { get; }
    }
}