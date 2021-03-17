using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginDevicesService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LogIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Certificate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.LoginId);
                });

            migrationBuilder.CreateTable(
                name: "LoginDevices",
                columns: table => new
                {
                    LoginDeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NeedUpdateDevice = table.Column<bool>(type: "bit", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDevices", x => x.LoginDeviceId);
                    table.ForeignKey(
                        name: "FK_LoginDevices_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "DeviceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoginDevices_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Certificate", "GroupId", "LogIn", "Password", "UserId" },
                values: new object[] { new Guid("fa3585fd-b259-4bb5-a37e-ce917fd13154"), "BwIAAACkAABSU0EyAAQAAAEAAQBFC1xx6QxReIJCpx9XNDgEoxsIhnZq2ibDCHKV8xS+UeHQHspO4dcXNALbiDx2BKou685I9aFjVav8Q+65Yo9IKgwS0e0j0hB+hA1DAGGe8/a6svrHTemiM7uToPVD8NNJO8l+a+uH/UZnb5O63xJnVfCUnvXQwM0205taBdWx5Y/m1xVd+E7QkzqChqniKMX2nkTBIInCfRLJillsUWTs6OwDgiRFNTKpfrgo8Z6P/psrnClXzV9LSEWO5mBGbPjrOqHmeacQzHULvQZbZTZqo/D9A35tw+zuMb6q5Lukwh2kwgQa+SDe3TJoNpbVMSwUsUSUjThbUl9ql9jiUrPshe3p+EXtAT+hO2p+rJ1WCEs5x6hsK9AP87BBGJmfyO4q3sN5TBOPQ2XLa6UXNHNxOGS+IFKPOu+alkdoxXZHPmE7xvuBBhtJVHXm228pj2mkL22OtheA333w0CGPBOF1BWZ0EPB3+cYG6agYKiSOSlu4CkiwvNR4iEBrzT/n3QCDJ61rW+ALiOQhRHMhO0Vs+YPwy1Iztl15wwaVQFEtQVx3+MjszHzpm+XrNMTuswcUHTpMqjfurYDgaza4n4iuHTge0jnk3+XQQgfYI6EP+Ag+2/X5rPnsqtNtxsNZGLQMcROdzGOPUknI8fZL9rY0gK+GH/FRAsY4C0qBTab7n1Nb1VcPmaMmo78hGLAc9kK2Yxi4JXv/FmYUGAdWCm3gRkTZhtGUdDqIiQTbybl/p++g5kHextX1kJDSWSw8Nr8=", new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"), "Admin", "827ccb0eea8a706c4c34a16891f84e7b", new Guid("a1972983-5389-49c5-a64d-bc4a65861a53") });

            migrationBuilder.CreateIndex(
                name: "IX_LoginDevices_DeviceId",
                table: "LoginDevices",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginDevices_LoginId",
                table: "LoginDevices",
                column: "LoginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginDevices");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Logins");
        }
    }
}
