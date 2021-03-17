using BaseDbContext;
using DevicePlatformEntity;
using DeviceService;
using GroupService;
using LoginService.Models;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace LoginDevicesService.Models
{
    public class LoginDevicesDbContext : BaseDatabaseContext, ILoginDevicesDbContext
    {
        public LoginDevicesDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions, "LoginDevicesDb")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<DevicePlatform> DevicePlatforms { get; set; }
        
        public DbSet<Login> Logins { get; set; }

        public DbSet<Device> Devices { get; set; }

        public DbSet<LoginDevices> LoginDevices { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<User>().Ignore<Group>().Ignore<DevicePlatform>();
            LoginDbContext.InitDbContext(modelBuilder, false);
            DeviceDbContext.InitDbContext(modelBuilder, false);
            modelBuilder.Entity<LoginDevices>().HasOne(x => x.LdLogin);
            modelBuilder.Entity<LoginDevices>().HasOne(x => x.LdDevice);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}