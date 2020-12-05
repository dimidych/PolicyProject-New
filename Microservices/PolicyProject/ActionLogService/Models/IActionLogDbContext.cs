using Microsoft.EntityFrameworkCore;

namespace ActionLogService.Models
{
    public interface IActionLogDbContext
    {
        DbSet<ActionLog> ActionLogs { get; set; }

        DbSet<EventAction> EventActions { get; set; }
    }
}