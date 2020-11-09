using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DeviceService.Migrations
{
    public partial class Add_timestamp_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "DevicePlatforms",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "DevicePlatforms");
        }
    }
}
