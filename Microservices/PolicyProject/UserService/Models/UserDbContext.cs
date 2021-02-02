using BaseDbContext;
using InitialConstantsStore;
using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public class UserDbContext : BaseDatabaseContext, IUserDbContext
    {
        public UserDbContext(DbContextOptions contextOptions) : base(contextOptions, "UserDb")
        {
        }

        public DbSet<User> Users { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserId = ConstStore.AdminUserIdGuid,
                UserLastName = ConstStore.AdminLogIn,
                UserFirstName = ConstStore.AdminLogIn
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}