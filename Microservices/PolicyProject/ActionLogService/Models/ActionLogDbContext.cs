using BaseDbContext;
using Microsoft.EntityFrameworkCore;

namespace ActionLogService.Models
{
    public class ActionLogDbContext : BaseDatabaseContext, IActionLogDbContext
    {
        public ActionLogDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions,
            "ActionLogDb")
        {
        }

        public DbSet<ActionLog> ActionLogs { get; set; }

        public DbSet<EventAction> EventActions { get; set; }

        public static void InitDbContext(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventAction>().HasData(
                new EventAction {EventId = 1, EventName = "Создание документа"},
                new EventAction {EventId = 2, EventName = "Проведение документа"},
                new EventAction {EventId = 3, EventName = "Сохранение документа"},
                new EventAction {EventId = 4, EventName = "Удаление документа"},
                new EventAction {EventId = 5, EventName = "Открытие документа"},
                new EventAction {EventId = 6, EventName = "Добавление пользователя"},
                new EventAction {EventId = 7, EventName = "Удаление пользователя"},
                new EventAction {EventId = 8, EventName = "Изменение пользователя"},
                new EventAction {EventId = 9, EventName = "Добавление логина"},
                new EventAction {EventId = 10, EventName = "Изменение логина"},
                new EventAction {EventId = 11, EventName = "Удаление логина"},
                new EventAction {EventId = 12, EventName = "Добавление устройства"},
                new EventAction {EventId = 13, EventName = "Изменение устройства"},
                new EventAction {EventId = 14, EventName = "Удаление устройства"},
                new EventAction {EventId = 15, EventName = "Выгрузка сертификата"},
                new EventAction {EventId = 16, EventName = "Добавлеине события"},
                new EventAction {EventId = 17, EventName = "Изменение события"},
                new EventAction {EventId = 18, EventName = "Удаление события"},
                new EventAction {EventId = 19, EventName = "Добавлеине группы"},
                new EventAction {EventId = 20, EventName = "Изменение группы"},
                new EventAction {EventId = 21, EventName = "Удаление группы"},
                new EventAction {EventId = 22, EventName = "Вход в систему"},
                new EventAction {EventId = 23, EventName = "Добавление политики"},
                new EventAction {EventId = 24, EventName = "Изменение политики"},
                new EventAction {EventId = 25, EventName = "Удаление политики"},
                new EventAction {EventId = 26, EventName = "Привязка устройства логину"},
                new EventAction {EventId = 27, EventName = "Отвязка устройства от логина"},
                new EventAction {EventId = 28, EventName = "Привязка политики логину"},
                new EventAction {EventId = 29, EventName = "Отвязка политики от логина"},
                new EventAction {EventId = 30, EventName = "Выход из системы"},
                new EventAction {EventId = 31, EventName = "Ошибка"},
                new EventAction {EventId = 32, EventName = "Удаление логов"}
            );
            modelBuilder.Entity<ActionLog>().HasOne(action => action.Action);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            InitDbContext(modelBuilder);
        }
    }
}