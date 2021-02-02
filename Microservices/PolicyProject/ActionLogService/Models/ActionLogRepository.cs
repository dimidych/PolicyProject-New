using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ActionLogService.Models
{
    public class ActionLogRepository : IActionLogRepository
    {
        private readonly IActionLogDbContext _dbContext;

        public ActionLogRepository(IActionLogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EventAction>> GetEventAction(int? eventActionId = null)
        {
            var result = new List<EventAction>();

            if (eventActionId == null)
                result = await _dbContext.EventActions.AsNoTracking().ToListAsync();
            else
                result.Add(await _dbContext.EventActions.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.EventId == eventActionId));

            return result;
        }

        public async Task<EventAction> AddEventAction(EventAction newEventAction)
        {
            if (newEventAction == null || string.IsNullOrEmpty(newEventAction.EventName))
                throw new ArgumentException("Событие не задано");

            var existedEvent = await _dbContext.EventActions.AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.EventName.Equals(newEventAction.EventName, StringComparison.InvariantCultureIgnoreCase));

            if (existedEvent != null)
                throw new Exception($"Событие {newEventAction.EventName} уже существует");

            newEventAction.EventId = _dbContext.EventActions.Any()
                ? await _dbContext.EventActions.MaxAsync(x => x.EventId) + 1
                : 1;
            var result = await _dbContext.EventActions.AddAsync(newEventAction);
            await (_dbContext as DbContext).SaveChangesAsync();
            return result.Entity;
        }

        public async Task<IEnumerable<ActionLog>> GetActionLog(DateTime fromDate, DateTime? toDate, string actionName,
            string login, string deviceSerialNumber, Guid? documentId, string messageFilter)
        {
            var finalDate = (toDate ?? DateTime.Now).AddDays(1);
            var tempQuery = _dbContext.ActionLogs.AsNoTracking()
                .Where(x => x.ActionLogDate >= fromDate && x.ActionLogDate <= finalDate);

            if (!string.IsNullOrEmpty(actionName))
                tempQuery = tempQuery.Where(x =>
                    x.EventActionId == _dbContext.EventActions.AsNoTracking().FirstOrDefault(y =>
                        y.EventName.Equals(actionName, StringComparison.InvariantCultureIgnoreCase)).EventId);

            if (!string.IsNullOrEmpty(login))
                tempQuery = tempQuery.Where(x => x.Login.Equals(login, StringComparison.InvariantCultureIgnoreCase));

            if (!string.IsNullOrEmpty(deviceSerialNumber))
                tempQuery = tempQuery.Where(x =>
                    x.DeviceSerialNumber.Equals(deviceSerialNumber, StringComparison.InvariantCultureIgnoreCase));

            if (documentId != null)
                tempQuery = tempQuery.Where(x => x.DocumentId == documentId);

            if (!string.IsNullOrEmpty(messageFilter))
                tempQuery = tempQuery.Where(x => x.Message.ToLower().Contains(messageFilter.ToLower()));

            var result = await tempQuery.ToListAsync();
            return result;
        }

        public async Task<ActionLog> AddActionLog(ActionLog newActionLog)
        {
            if (newActionLog == null || string.IsNullOrEmpty(newActionLog.Message))
                throw new ArgumentException("Событие не задано");

            if (newActionLog.EventActionId < 1)
                throw new ArgumentException("Не указан тип события");

            if (string.IsNullOrEmpty(newActionLog.Login))
                throw new ArgumentException("Не указан инициатор события");

            newActionLog.ActionLogDate = DateTime.Now;
            newActionLog.ActionLogId = Guid.NewGuid();
            var result = await _dbContext.ActionLogs.AddAsync(newActionLog);
            await (_dbContext as DbContext).SaveChangesAsync();
            return result.Entity;
        }
    }
}