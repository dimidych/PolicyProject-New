using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeviceService
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetDevice(long? deviceId=null);
        Task<Device> AddDevice(Device newDevice);
        Task<bool> UpdateDevice(Device device);
        Task<bool> DeleteDevice(int deviceId);
    }
}