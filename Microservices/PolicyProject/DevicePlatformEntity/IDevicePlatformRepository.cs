using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevicePlatformEntity
{
    public interface IDevicePlatformRepository
    {
        Task<IEnumerable<DevicePlatform>> GetDevicePlatform(short? devicePlatformId = null);
        Task<DevicePlatform> AddDevicePlatform(DevicePlatform newDevicePlatform);
    }
}