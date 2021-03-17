using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LoginDevicesService.Models
{
    public interface ILoginDevicesRepository
    {
        Task<IEnumerable<LoginDevices>> GetDevicesOfLogin(string login);
        Task<Dictionary<Guid, Guid[]>> GetLoginIdAndDeviceIdDct();
        Task<bool> UpdateLoginDevices(Guid loginId, LoginDevices[] loginDevicesList);
        Task<bool> SetDevicesForUpdate(List<Guid> loginIdList);
    }
}