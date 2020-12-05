using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolicyService.Migrations
{
    public partial class Initial : Migration
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
                    PolicyId = table.Column<int>(type: "int", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformId = table.Column<short>(type: "smallint", nullable: false),
                    PolicyInstruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyDefaultParam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_Policies_DevicePlatforms_PlatformId",
                        column: x => x.PlatformId,
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
                name: "IX_Policies_PlatformId",
                table: "Policies",
                column: "PlatformId");
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
