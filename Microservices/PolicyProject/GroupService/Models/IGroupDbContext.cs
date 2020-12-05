using Microsoft.EntityFrameworkCore;

namespace GroupService
{
    public interface IGroupDbContext
    {
        DbSet<Group> Groups { get; set; }
    }
}