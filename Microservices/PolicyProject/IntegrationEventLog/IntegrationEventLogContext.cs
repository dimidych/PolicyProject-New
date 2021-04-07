using BaseDbContext;
using Microsoft.EntityFrameworkCore;

namespace IntegrationEventLog
{
    public class IntegrationEventLogContext :BaseDatabaseContext
    {
        public IntegrationEventLogContext(DbContextOptions dbContextOptions) : base(dbContextOptions,
            "IntegrationEventLogDb")
        {
        }

        public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }
    }
}
