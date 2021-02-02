using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActionLogService.Models
{
    public interface IActionLogRepository
    {
        Task<IEnumerable<EventAction>> GetEventAction(int? eventActionId = null);
        Task<EventAction> AddEventAction(EventAction newEventAction);
        Task<IEnumerable<ActionLog>> GetActionLog(DateTime fromDate, DateTime? toDate, string action,
            string login, string deviceSerialNumber, Guid? documentId, string messageFilter);
        Task<ActionLog> AddActionLog(ActionLog newActionLog);
    }
}