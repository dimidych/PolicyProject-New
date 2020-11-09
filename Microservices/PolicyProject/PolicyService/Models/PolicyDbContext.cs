using DevelopmentLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PolicyService.Models
{
    public class PolicyDbContext : DbContext
    {
        public PolicyDbContext(DbContextOptions<PolicyDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<DevicePlatform> DevicePlatforms { get; set; }

        public DbSet<Policy> Policies { get; set; }

        public static readonly ILoggerFactory MyDevelopmentLoggerFactory
            = LoggerFactory.Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                && level == LogLevel.Information)
                .AddProvider(new CustomDevelopmentLoggerProvider("PolicyDb.log")));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyDevelopmentLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Policy>().HasOne(x => x.Platform);
        }
    }
}