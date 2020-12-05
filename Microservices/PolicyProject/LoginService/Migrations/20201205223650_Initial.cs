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
                    GroupId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<long>(type: "bigint", nullable: false),
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
                    LoginId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false),
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
                values: new object[] { 1, "Админ" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserFirstName", "UserLastName", "UserMiddleName" },
                values: new object[] { 1L, "Admin", "Admin", null });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Certificate", "GroupId", "LogIn", "Password", "UserId" },
                values: new object[] { 1L, "BwIAAACkAABSU0EyAAQAAAEAAQDRswwNdIPlkefrwLI43ZLI1RsQtyaIZtiBuhKDqsALTFaZh92DFqdkRgXDl9uPgKD6ryF+T6zt5FwgLn3KjeMxp9q3Kx3MJBBQWvpTjlCyu9mF9Z+JSL5qUBaALXwrfz7N6cKCR9BDwcRfE0EdjPr9jmQxSIvVOQurJqODCBEPyEduGppARQacpvT2+eQf7FaUkdBMabsw6S1wmmFmR1Tc/tHcYI+oEvLa/BOiOZL6aJt4ToGAWdX6C/3PkMeHsM8nYVvE812WVqcHpThJJuvLt6RV/KSq3GJlFR3OmWMyz5AKawPoaC3Tb8XFT3lSw9T2QQb8Iw5urybC1jypJJj2CzZ7vPOU1Zy42J3ibRC6+Xq89yto5zHuNxoD9XfhToIdfOIL05FKccBmNJEWCnRutiwaiWX/jc92V+ccB/97Fhc/3uZltBt0C9aJph4TT7h6WpAl1JWBT4mhI492pp602LZE8Nkd7XlgmNPNCYZwfO5/n58Fw5RJ9K8LdSsDgkVUqKb4yy44bdKZrQRxhF5m06PjlyC6qBU79Rs9hFqbx79+QbxeEf16VG99kqWi4M2qhySuvB1BQu4UDgBcofFNZTJMDe+o8W3B6N4i8D7h+4xZG1yDx4e8q9KyOTlRdq8QfulSDrt3EaEnBrKMeUXdSXC1yI6GjqfTayhsSv4K/Fc5xxE3UQtGQe7Vsw0gjgzkwwmSrqX8LnbMyKNMTwyV8RWWaEg0HGVwsOtys7KEn0pdxv4metkuswMyEwmKnhg=", 1, "Admin", "827ccb0eea8a706c4c34a16891f84e7b", 1L });

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
