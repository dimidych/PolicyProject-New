using BaseDbContext;
using GroupService;
using InitialConstantsStore;
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
                LoginId = ConstStore.AdminLoginIdGuid,
                GroupId = ConstStore.AdminGroupIdGuid,
                UserId = ConstStore.AdminUserIdGuid,
                LogIn = ConstStore.AdminLogIn,
                Certificate = CertificateWorker.CreateCertificate(),
                Password = Hasher.Hash(ConstStore.AdminSecure)
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}