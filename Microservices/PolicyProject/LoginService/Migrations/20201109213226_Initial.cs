using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoginService.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false),
                    GroupName = table.Column<string>(nullable: false),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    LoginId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    LogIn = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Certificate = table.Column<string>(nullable: true),
                    Timestamp = table.Column<byte[]>(rowVersion: true, nullable: true)
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
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserName" },
                values: new object[] { 1L, "Admin" });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Certificate", "GroupId", "LogIn", "Password", "UserId" },
                values: new object[] { 1L, "BwIAAACkAABSU0EyAAQAAAEAAQD7UC1a+XSlxZZEOAJBXoCsNzTHC4BbE7yCBQGSdaThAI1tEBOlkorPC3BR5znSghSYoEi5ObTTAk7P4FBwMYwZR/dFpyZPAH0jpRK387oCH1fmvZX9QQhtTjX8Pxp797h67UW+FoxhpZsXk6AHJ5uKVSPiQ5aFpxrLLjOsLZgjmRmilI2rvBndG6u4jGCAkIK6aJcgkAI82jXBu5/tL5/8LMgeJP4k7Kkwq9GxOQ9TVyxLQ3BfsbkxiXpvC9Ns4sAz9tVrdG1wFFjgN46RjxaY2z5v0EVGFK4kDETKmOmphVTDphJp0PKg6dL3am8JvUXO3rjhYiR/+EU1d9GkxD/LMb3v9pJuoYkLTmYilkcWTbwZOoHBuKOMRlI+ULiphoaAoYJL7BtyK9BdR+jmdUl8PDWWYRK9r9XWw6v+7Sqftis/6bSDomSRYa6tLaRXRuigc4BddGNmiDkDfqyNuEM8dOyr775Qiw6QLH15HqZ9GqCv6FQR3JWrgn/9x5KeZHUuGDmRBmfKuEyOWNQzrjDX9ww+cNBNhKLu8vhkYTIW2ioCouzJi7h97b/jXX4avR4p0vVWMp6q47DaqTyT+yaZca1u0vmm1sazN6j9yw8UbKd6KFevrMkUfSyHpOu1D2jvFsreo9tdLYzhs731bWDVA0Vlk4l1pTtSqTjv+NjY13Wd50rr/V1VDcrR8lZ52dVebNnYSh/FHicOzxVMM7WKxIFQedxqdH95EjRNMk4Vn3Souk5pgVWXhmzy6cdfbAQ=", 1, "Admin", "827ccb0eea8a706c4c34a16891f84e7b", 1L });

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
