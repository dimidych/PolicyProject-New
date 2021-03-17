using BaseDbContext;
using DevicePlatformEntity;
using Microsoft.EntityFrameworkCore;

namespace PolicyService.Models
{
    public class PolicyDbContext : BaseDatabaseContext, IPolicyDbContext
    {
        public PolicyDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions, "PolicyDb")
        {
        }

        public DbSet<DevicePlatform> DevicePlatforms { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder, bool initializeDerived)
        {
            if (!initializeDerived)
                return;

            DevicePlatformDbContext.InitDbContext(modelBuilder);
            modelBuilder.Entity<Policy>().HasOne(device => device.DevicePlatform);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder, true);
        }
    }
}