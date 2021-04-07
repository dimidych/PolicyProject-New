using BaseDbContext;
using DevicePlatformEntity;
using GroupService;
using LoginService.Models;
using Microsoft.EntityFrameworkCore;
using PolicyService;
using PolicyService.Models;
using UserService.Models;

namespace PolicySetService.Models
{
    public class PolicySetDbContext : BaseDatabaseContext, IPolicySetDbContext
    {
        public PolicySetDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions, "PolicySetDb")
        {
        }

        public DbSet<DevicePlatform> DevicePlatforms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Policy> Policies { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Login> Logins { get; set; }
        public DbSet<PolicySet> PolicySets { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<DevicePlatform>().Ignore<User>();
            PolicyDbContext.InitDbContext(modelBuilder, false);
            LoginDbContext.InitDbContext(modelBuilder, false);
            GroupDbContext.InitDbContext(modelBuilder);
            modelBuilder.Entity<PolicySet>().HasOne(x => x.PolicySetPolicy);
            modelBuilder.Entity<PolicySet>().HasOne(x => x.UserLogin);
            modelBuilder.Entity<PolicySet>().HasOne(x => x.PolicySetGroup);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}