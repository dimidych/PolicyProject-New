using DevelopmentLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoginService.Models
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Login> Logins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }

        public static readonly ILoggerFactory MyDevelopmentLoggerFactory
            = LoggerFactory.Create(builder => builder
                .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name
                                                && level == LogLevel.Information)
                .AddProvider(new CustomDevelopmentLoggerProvider("LoginDb.log")));

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyDevelopmentLoggerFactory);
        }
    }
}