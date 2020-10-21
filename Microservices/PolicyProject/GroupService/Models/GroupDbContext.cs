using DevelopmentLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GroupService
{
    public class GroupDbContext : DbContext
    {
        public GroupDbContext(DbContextOptions<GroupDbContext> contextOptions) : base(contextOptions)
        {}

        public DbSet<Group> Groups { get; set; }

        public static readonly ILoggerFactory MyDevelopmentLoggerFactory
            = LoggerFactory.Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                && level == LogLevel.Information)
                .AddProvider(new CustomDevelopmentLoggerProvider("GroupDb.log")));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyDevelopmentLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasData(new Group {GroupId = 1, GroupName = "Админ"});
        }
    }
}