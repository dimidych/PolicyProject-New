using BaseDbContext;
using GroupService;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace LoginService.Models
{
    public class LoginDbContext : BaseDatabaseContext, ILoginDbContext
    {
        public LoginDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions, "LoginDb")
        {
        }

        public DbSet<Login> Logins { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            UserDbContext.InitDbContext(modelBuilder);
            GroupDbContext.InitDbContext(modelBuilder);
            modelBuilder.Entity<Login>().HasOne(x => x.User);
            modelBuilder.Entity<Login>().HasOne(x => x.Group);
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}