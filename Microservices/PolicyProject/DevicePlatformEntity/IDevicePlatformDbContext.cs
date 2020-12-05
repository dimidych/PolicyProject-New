using Microsoft.EntityFrameworkCore;

namespace DevicePlatformEntity
{
    public interface IDevicePlatformDbContext
    {
        DbSet<DevicePlatform> DevicePlatforms { get; set; }
    }
}