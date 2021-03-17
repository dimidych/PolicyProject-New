using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LoginDevicesService.Models
{
    public class LoginDevicesRepository : ILoginDevicesRepository
    {
        private readonly ILoginDevicesDbContext _dbContext;

        public LoginDevicesRepository(ILoginDevicesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<IEnumerable<LoginDevices>> GetDevicesOfLogin(string login)
        {
            var result = _dbContext.LoginDevices.AsNoTracking().Where(x =>
                    x.LdLogin.LogIn.Equals(login, StringComparison.InvariantCultureIgnoreCase))
                .AsEnumerable();
            return Task.FromResult(result);
        }

        public Task<Dictionary<Guid, Guid[]>> GetLoginIdAndDeviceIdDct()
        {
            var devices = from dev in _dbContext.LoginDevices.AsNoTracking()
                group dev by dev.LoginId
                into selectedDevice
                select new
                {
                    LoginId = selectedDevice.Key,
                    DevIds = selectedDevice.Select(x => x.DeviceId)
                };
            var resultDct = new Dictionary<Guid, Guid[]>();

            foreach (var devs in devices)
                resultDct.Add(devs.LoginId, devs.DevIds.ToArray());

            return Task.FromResult(resultDct);
        }

        public async Task<bool> UpdateLoginDevices(Guid loginId, LoginDevices[] loginDevicesList)
        {
            if (loginId == Guid.Empty)
                throw new ArgumentException("Не задан логин");

            if (loginDevicesList == null || !loginDevicesList.Any())
                throw new ArgumentException("Пустой список устройств");

            var nativeDbContext = _dbContext as DbContext;

            using (var transaction = await nativeDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var deletedList = new List<LoginDevices>();

                    foreach (var loginDevice in _dbContext.LoginDevices.Where(x => x.LoginId == loginId))
                        if (loginDevicesList.All(x => x.DeviceId != loginDevice.DeviceId))
                            deletedList.Add(loginDevice);

                    _dbContext.LoginDevices.RemoveRange(deletedList);

                    foreach (var selectedDevice in loginDevicesList)
                    {
                        if (
                            _dbContext.LoginDevices.Any(
                                x => x.DeviceId == selectedDevice.DeviceId && x.LoginId == selectedDevice.LoginId))
                            continue;

                        _dbContext.LoginDevices.Add(new LoginDevices
                        {
                            LoginDeviceId = Guid.NewGuid(),
                            DeviceId = selectedDevice.DeviceId,
                            LoginId = selectedDevice.LoginId,
                            NeedUpdateDevice = selectedDevice.NeedUpdateDevice
                        });
                    }

                    var result = await nativeDbContext.SaveChangesAsync() > 0;

                    if (result)
                        await transaction.CommitAsync();

                    return result;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> SetDevicesForUpdate(List<Guid> loginIdList)
        {
            if (loginIdList == null || !loginIdList.Any())
                throw new Exception("Пустой список устройств для обновления");

            await _dbContext.LoginDevices.ForEachAsync(x => x.NeedUpdateDevice = true);
            return await (_dbContext as DbContext).SaveChangesAsync() > 0;
        }
    }
}