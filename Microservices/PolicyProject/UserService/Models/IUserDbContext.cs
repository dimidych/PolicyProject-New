using Microsoft.EntityFrameworkCore;

namespace UserService.Models
{
    public interface IUserDbContext
    {
        DbSet<User> Users { get; set; }
    }
}