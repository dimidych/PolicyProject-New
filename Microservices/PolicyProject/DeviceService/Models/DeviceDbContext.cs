using DevelopmentLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DeviceService
{
    public class DeviceDbContext : DbContext
    {
        public DeviceDbContext(DbContextOptions<DeviceDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Device> Devices { get; set; }

        public DbSet<DevicePlatform> DevicePlatforms { get; set; }

        public static readonly ILoggerFactory MyDevelopmentLoggerFactory
            = LoggerFactory.Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                && level == LogLevel.Information)
                .AddProvider(new CustomDevelopmentLoggerProvider("DeviceDb.log")));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyDevelopmentLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DevicePlatform>().HasData(
                new DevicePlatform {DevicePlatformId = 1, DevicePlatformName = "Android"},
                new DevicePlatform {DevicePlatformId = 2, DevicePlatformName = "IOS"},
                new DevicePlatform {DevicePlatformId = 3, DevicePlatformName = "Windows"},
                new DevicePlatform {DevicePlatformId = 4, DevicePlatformName = "Linux"}
            );
            modelBuilder.Entity<Device>().HasOne(device => device.DevicePlatform);
        }
    }
}