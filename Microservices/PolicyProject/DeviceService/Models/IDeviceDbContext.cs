using DevicePlatformEntity;
using Microsoft.EntityFrameworkCore;

namespace DeviceService
{
    public interface IDeviceDbContext : IDevicePlatformDbContext
    {
        DbSet<Device> Devices { get; set; }
    }
}