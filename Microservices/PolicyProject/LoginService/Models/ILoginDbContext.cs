using GroupService;
using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace LoginService.Models
{
    public interface ILoginDbContext : IUserDbContext, IGroupDbContext
    {
        DbSet<Login> Logins { get; set; }
    }
}