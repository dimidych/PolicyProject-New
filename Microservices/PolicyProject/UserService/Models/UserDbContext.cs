using DevelopmentLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserService.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> contextOptions) : base(contextOptions) { }

        public DbSet<User> Users { get; set; }

        public static readonly ILoggerFactory MyDevelopmentLoggerFactory
            = LoggerFactory.Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                && level == LogLevel.Information)
                .AddProvider(new CustomDevelopmentLoggerProvider("UserDb.log")));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyDevelopmentLoggerFactory);
        }
    }
}