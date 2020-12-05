using BaseDbContext;
using Microsoft.EntityFrameworkCore;

namespace DevicePlatformEntity
{
    public class DevicePlatformDbContext : BaseDatabaseContext, IDevicePlatformDbContext
    {
        public DevicePlatformDbContext(DbContextOptions dbContextOptions) : base(
            dbContextOptions, "DevicePlatformDb")
        {
        }

        public DbSet<DevicePlatform> DevicePlatforms { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevicePlatform>().HasData(
                new DevicePlatform { DevicePlatformId = 1, DevicePlatformName = "Android" },
                new DevicePlatform { DevicePlatformId = 2, DevicePlatformName = "IOS" },
                new DevicePlatform { DevicePlatformId = 3, DevicePlatformName = "Windows" },
                new DevicePlatform { DevicePlatformId = 4, DevicePlatformName = "Linux" }
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}