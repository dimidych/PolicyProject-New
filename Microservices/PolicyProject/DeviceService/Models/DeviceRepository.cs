using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeviceService
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly IDeviceDbContext _dbContext;

        public DeviceRepository(IDeviceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Device>> GetDevice(Guid? deviceId = null)
        {
            var result = new List<Device>();

            if (deviceId == null)
                result = await _dbContext.Devices.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.Devices.AsNoTracking().FirstOrDefaultAsync(x => x.DeviceId == deviceId));

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

            newDevice.DeviceId = Guid.NewGuid();
            var result = await _dbContext.Devices.AddAsync(newDevice);
            await (_dbContext as DbContext).SaveChangesAsync();
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
            var updated = await (_dbContext as DbContext).SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> DeleteDevice(Guid deviceId)
        {
            var existedDevice = await _dbContext.Devices.FirstOrDefaultAsync(x => x.DeviceId == deviceId);

            if (existedDevice == null)
                throw new Exception($"Устройство c id {deviceId} не существует");

            _dbContext.Devices.Remove(existedDevice);
            var deleted = await (_dbContext as DbContext).SaveChangesAsync();
            return deleted > 0;
        }
    }
}