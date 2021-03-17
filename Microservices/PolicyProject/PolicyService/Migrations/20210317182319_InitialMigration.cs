using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolicyService.Migrations
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
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformId = table.Column<short>(type: "smallint", nullable: false),
                    PolicyInstruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyDefaultParam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DevicePlatformId = table.Column<short>(type: "smallint", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_Policies_DevicePlatforms_DevicePlatformId",
                        column: x => x.DevicePlatformId,
                        principalTable: "DevicePlatforms",
                        principalColumn: "DevicePlatformId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Policies_DevicePlatformId",
                table: "Policies",
                column: "DevicePlatformId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Policies");

            migrationBuilder.DropTable(
                name: "DevicePlatforms");
        }
    }
}
