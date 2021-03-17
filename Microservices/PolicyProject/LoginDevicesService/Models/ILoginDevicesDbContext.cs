using DeviceService;
using LoginService.Models;
using Microsoft.EntityFrameworkCore;

namespace LoginDevicesService.Models
{
    public interface ILoginDevicesDbContext : ILoginDbContext, IDeviceDbContext
    {
        DbSet<LoginDevices> LoginDevices { get; set; }
    }
}