using BaseDbContext;
using DevicePlatformEntity;
using Microsoft.EntityFrameworkCore;

namespace DeviceService
{
    public class DeviceDbContext : BaseDatabaseContext, IDeviceDbContext
    {
        public DeviceDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions, "DeviceDb")
        {
        }

        public DbSet<Device> Devices { get; set; }

        public DbSet<DevicePlatform> DevicePlatforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            DevicePlatformDbContext.InitDbContext(modelBuilder);
            modelBuilder.Entity<Device>().HasOne(device => device.DevicePlatform);
        }
    }
}