using DevelopmentLogger;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LoginService.Models
{
    public class LoginDbContext : DbContext
    {
        public LoginDbContext(DbContextOptions<LoginDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>().HasOne(x => x.User);
            modelBuilder.Entity<Login>().HasOne(x => x.Group);
            modelBuilder.Entity<Group>().HasData(new Group {GroupId = 1, GroupName = "Admin"});
            modelBuilder.Entity<User>().HasData(new User {UserId = 1, UserName = "Admin"});
            modelBuilder.Entity<Login>().HasData(new Login
            {
                LoginId = 1,
                GroupId = 1,
                UserId = 1,
                LogIn = "Admin",
                Certificate = CertificateWorker.CreateCertificate(),
                Password = Hasher.Hash("12345")
            });
        }
    }
}