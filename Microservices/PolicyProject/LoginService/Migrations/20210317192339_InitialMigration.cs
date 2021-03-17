using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginService.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
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
                    table.ForeignKey(
                        name: "FK_Logins_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Logins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "GroupName" },
                values: new object[] { new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"), "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserFirstName", "UserLastName", "UserMiddleName" },
                values: new object[] { new Guid("a1972983-5389-49c5-a64d-bc4a65861a53"), "Admin", "Admin", null });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Certificate", "GroupId", "LogIn", "Password", "UserId" },
                values: new object[] { new Guid("fa3585fd-b259-4bb5-a37e-ce917fd13154"), "BwIAAACkAABSU0EyAAQAAAEAAQC1EzUrc1PDqwnFsfX5YByYLcdyZZsv5arMReG2Hf5QPHYdOOhlrvcfANW45Ex32F8Tq0AFQyjgh1tIdWLTmKicTJnRuYjq5DzZIa3o6er6t+JkKFaaVQI7gk4mI7HrmufYvzJDuWxiFoogm+uUqnT3YKxmkbUYODtTZjJlpy302mddZbjdVb0zwxUxSQt2M2w/6GA4KhYp76z9UwV0LqBRIBgIL1jhhrXlrUXrMaysj0fmxSJMr6/nnTNOIz74DuSDeEKxaCV9EX2jdm7k6iUBejWQfATxrrmKdEDfYA+2mdWbDex3ZMuCnIPVeqUn3lijDdRjgX+eJx/uWBJipMf1IYB2Yhwk3/CtKBrG9IIpJnMcloYTbFFl5/cx/Qsg9mrlIVxyT+/qRVr9xxXKJK6FXaSOJm57+IbYwDs1R7HcPyOwncUs5Au9GA6K3GXQEGEAXj9ZiDlDwbM0z/0ZirgzgvZMOpQL5ZGB2cJWwpuPU42HDkq+3fQ/a7D9EXWXq2PgirBFQulsK5nHalEAGRxsRq8wWyiIomfrSCFSjSRldaRr4GP1FzADg5TUsp6qYX0eCB9Y2Ea95Avcw1+C0Hdi5a2FaXzNobCOo3EfgNDwig7WJbVm/fP6QRcZqLcG+TpI0J8h9yzkVvZDc5/qdoi4Y2i+g6bUvbLLzcv3pSSem/l+pHnz+PigInv/Z2taTt2qgPsOV2rFNTvgTVhiztGUMV/Q3c13WVBdEssFBiAHztzaKsmBM0wWsdvwQRDerKk=", new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"), "Admin", "827ccb0eea8a706c4c34a16891f84e7b", new Guid("a1972983-5389-49c5-a64d-bc4a65861a53") });

            migrationBuilder.CreateIndex(
                name: "IX_Logins_GroupId",
                table: "Logins",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
