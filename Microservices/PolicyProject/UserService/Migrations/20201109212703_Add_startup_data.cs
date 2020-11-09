using Microsoft.EntityFrameworkCore.Migrations;

namespace UserService.Migrations
{
    public partial class Add_startup_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserFirstName", "UserLastName", "UserMiddleName" },
                values: new object[] { 1L, "Admin", "Admin", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1L);
        }
    }
}
