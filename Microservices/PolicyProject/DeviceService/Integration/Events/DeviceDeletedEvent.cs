using System;
using EventBus;

namespace DeviceService.Integration.Events
{
    public class DeviceDeletedEvent : IntegrationEvent
    {
        public DeviceDeletedEvent(Guid deviceId)
        {
            DeviceId = deviceId;
        }

        public Guid DeviceId { get; }
    }
}