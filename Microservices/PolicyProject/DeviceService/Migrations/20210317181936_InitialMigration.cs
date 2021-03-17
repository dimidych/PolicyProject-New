using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevicePlatforms",
                columns: table => new
                {
                    DevicePlatformId = table.Column<short>(type: "smallint", nullable: false),
                    DevicePlatformName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevicePlatforms", x => x.DevicePlatformId);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceSerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceMacAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceIpAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DevicePlatformId = table.Column<short>(type: "smallint", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_Devices_DevicePlatforms_DevicePlatformId",
                        column: x => x.DevicePlatformId,
                        principalTable: "DevicePlatforms",
                        principalColumn: "DevicePlatformId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DevicePlatforms",
                columns: new[] { "DevicePlatformId", "DevicePlatformName" },
                values: new object[,]
                {
                    { (short)1, "Android" },
                    { (short)2, "IOS" },
                    { (short)3, "Windows" },
                    { (short)4, "Linux" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DevicePlatformId",
                table: "Devices",
                column: "DevicePlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "DevicePlatforms");
        }
    }
}
