using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ActionLogService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventActions",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false),
                    EventName = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventActions", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    ActionLogId = table.Column<long>(nullable: false),
                    ActionLogDate = table.Column<DateTime>(nullable: false),
                    EventActionId = table.Column<int>(nullable: false),
                    DocumentId = table.Column<long>(nullable: true),
                    Message = table.Column<string>(nullable: false),
                    DeviceSerialNumber = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.ActionLogId);
                    table.ForeignKey(
                        name: "FK_ActionLogs_EventActions_EventActionId",
                        column: x => x.EventActionId,
                        principalTable: "EventActions",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "EventActions",
                columns: new[] { "EventId", "EventName" },
                values: new object[,]
                {
                    { 1, "Создание документа" },
                    { 30, "Выход из системы" },
                    { 29, "Отвязка политики от логина" },
                    { 28, "Привязка политики логину" },
                    { 27, "Отвязка устройства от логина" },
                    { 26, "Привязка устройства логину" },
                    { 25, "Удаление политики" },
                    { 24, "Изменение политики" },
                    { 23, "Добавление политики" },
                    { 22, "Вход в систему" },
                    { 21, "Удаление группы" },
                    { 20, "Изменение группы" },
                    { 19, "Добавлеине группы" },
                    { 18, "Удаление события" },
                    { 17, "Изменение события" },
                    { 16, "Добавлеине события" },
                    { 15, "Выгрузка сертификата" },
                    { 14, "Удаление устройства" },
                    { 13, "Изменение устройства" },
                    { 12, "Добавление устройства" },
                    { 11, "Удаление логина" },
                    { 10, "Изменение логина" },
                    { 9, "Добавление логина" },
                    { 8, "Изменение пользователя" },
                    { 7, "Удаление пользователя" },
                    { 6, "Добавление пользователя" },
                    { 5, "Открытие документа" },
                    { 4, "Удаление документа" },
                    { 3, "Сохранение документа" },
                    { 2, "Проведение документа" },
                    { 31, "Ошибка" },
                    { 32, "Удаление логов" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionLogs_EventActionId",
                table: "ActionLogs",
                column: "EventActionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "EventActions");
        }
    }
}
