using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeviceService
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly DeviceDbContext _dbContext;

        public DeviceRepository(DeviceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DevicePlatform> AddDevicePlatform(DevicePlatform newDevicePlatform)
        {
            if (newDevicePlatform == null || string.IsNullOrEmpty(newDevicePlatform.DevicePlatformName))
                throw new ArgumentException("Платформа не задана");

            var existed = await _dbContext.DevicePlatforms.AsNoTracking().FirstOrDefaultAsync(x =>
                x.DevicePlatformName.Equals(newDevicePlatform.DevicePlatformName,
                    StringComparison.InvariantCultureIgnoreCase));

            if (existed != null)
                throw new Exception($"Платформа {newDevicePlatform.DevicePlatformName} уже существует");

            newDevicePlatform.DevicePlatformId = (short) (_dbContext.DevicePlatforms.Any()
                ? await _dbContext.DevicePlatforms.MaxAsync(x => x.DevicePlatformId) + 1
                : 1);
            var result = await _dbContext.DevicePlatforms.AddAsync(newDevicePlatform);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<Device>> GetDevice(long? deviceId = null)
        {
            var result = new List<Device>();

            if (deviceId == null)
                result = await _dbContext.Devices.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.Devices.AsNoTracking().FirstOrDefaultAsync(x => x.DeviceId == deviceId));

            return result;
        }

        public async Task<IEnumerable<DevicePlatform>> GetDevicePlatform(short? devicePlatformId = null)
        {
            var result = new List<DevicePlatform>();

            if (devicePlatformId == null)
                result = await _dbContext.DevicePlatforms.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.DevicePlatforms.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.DevicePlatformId == devicePlatformId));

            return result;
        }

        public async Task<Device> AddDevice(Device newDevice)
        {
            if (newDevice == null || string.IsNullOrEmpty(newDevice.DeviceName))
                throw new ArgumentException("Устройство не задано");

            if (string.IsNullOrEmpty(newDevice.DeviceSerialNumber))
                throw new ArgumentException("Не указан серийный номер устройства");

            if (string.IsNullOrEmpty(newDevice.DeviceIpAddress))
                throw new ArgumentException("Не указан IP адрес устройства");

            if (string.IsNullOrEmpty(newDevice.DeviceMacAddress))
                throw new ArgumentException("Не указан Mac адрес устройства");

            if (newDevice.DevicePlatformId < 1)
                throw new ArgumentException("Не указана платформа устройства");

            var existedDevice = await _dbContext.Devices.AsNoTracking().FirstOrDefaultAsync(x =>
                x.DeviceName.Equals(newDevice.DeviceName, StringComparison.InvariantCultureIgnoreCase) &&
                x.DeviceSerialNumber.Equals(newDevice.DeviceSerialNumber, StringComparison.InvariantCultureIgnoreCase));

            if (existedDevice != null)
                throw new Exception($"Устройство {newDevice.DeviceName} уже существует");

            newDevice.DeviceId = _dbContext.Devices.Any()
                ? await _dbContext.Devices.MaxAsync(x => x.DeviceId) + 1
                : 1;
            var result = await _dbContext.Devices.AddAsync(newDevice);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> UpdateDevice(Device device)
        {
            if (device == null || string.IsNullOrEmpty(device.DeviceName))
                throw new ArgumentException("Устройство не задано");

            if (string.IsNullOrEmpty(device.DeviceSerialNumber))
                throw new ArgumentException("Не указан серийный номер устройства");

            if (string.IsNullOrEmpty(device.DeviceIpAddress))
                throw new ArgumentException("Не указан IP адрес устройства");

            if (string.IsNullOrEmpty(device.DeviceMacAddress))
                throw new ArgumentException("Не указан Mac адрес устройства");

            if (device.DevicePlatformId < 1)
                throw new ArgumentException("Не указана платформа устройства");

            var existedDevice = await _dbContext.Devices.FirstOrDefaultAsync(x =>
                x.DeviceName.Equals(device.DeviceName, StringComparison.InvariantCultureIgnoreCase) &&
                x.DeviceSerialNumber.Equals(device.DeviceSerialNumber, StringComparison.InvariantCultureIgnoreCase));

            if (existedDevice == null)
                throw new Exception($"Устройство {device.DeviceName} не существует");

            existedDevice.DeviceName = device.DeviceName;
            existedDevice.DeviceSerialNumber = device.DeviceSerialNumber;
            existedDevice.DeviceIpAddress = device.DeviceIpAddress;
            existedDevice.DeviceMacAddress = device.DeviceMacAddress;
            existedDevice.DevicePlatformId = device.DevicePlatformId;
            var updated = await _dbContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteDevice(int deviceId)
        {
            var existedDevice = await _dbContext.Devices.FirstOrDefaultAsync(x => x.DeviceId == deviceId);

            if (existedDevice == null)
                throw new Exception($"Устройство c id {deviceId} не существует");

            _dbContext.Remove(existedDevice);
            var deleted = await _dbContext.SaveChangesAsync();
            return deleted > 0;
        }
    }
}