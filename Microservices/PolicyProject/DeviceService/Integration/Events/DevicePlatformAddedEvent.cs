using DevicePlatformEntity;
using EventBus;

namespace DeviceService.Integration.Events
{
    public class DevicePlatformAddedEvent : IntegrationEvent
    {
        public DevicePlatformAddedEvent(DevicePlatform newPlatform)
        {
            NewPlatform = newPlatform;
        }

        public DevicePlatform NewPlatform { get; }
    }
}