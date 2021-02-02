using LoginService.Models;
using Microsoft.EntityFrameworkCore;
using PolicyService.Models;

namespace PolicySetService.Models
{
    public interface IPolicySetDbContext : IPolicyDbContext, ILoginDbContext
    {
        DbSet<PolicySet> PolicySets { get; set; }
    }
}