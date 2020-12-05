using DevicePlatformEntity;
using Microsoft.EntityFrameworkCore;

namespace PolicyService.Models
{
    public interface IPolicyDbContext : IDevicePlatformDbContext
    {
        DbSet<Policy> Policies { get; set; }
    }
}