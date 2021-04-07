using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolicySetService.Migrations
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
                name: "Policies",
                columns: table => new
                {
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlatformId = table.Column<short>(type: "smallint", nullable: false),
                    PolicyInstruction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyDefaultParam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.PolicyId);
                });

            migrationBuilder.CreateTable(
                name: "PolicySets",
                columns: table => new
                {
                    PolicySetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PolicyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Selected = table.Column<bool>(type: "bit", nullable: true),
                    PolicyParam = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySets", x => x.PolicySetId);
                    table.ForeignKey(
                        name: "FK_PolicySets_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicySets_Logins_LoginId",
                        column: x => x.LoginId,
                        principalTable: "Logins",
                        principalColumn: "LoginId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicySets_Policies_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "GroupId", "GroupName" },
                values: new object[] { new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"), "Admin" });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "LoginId", "Certificate", "GroupId", "LogIn", "Password", "UserId" },
                values: new object[] { new Guid("fa3585fd-b259-4bb5-a37e-ce917fd13154"), "BwIAAACkAABSU0EyAAQAAAEAAQAhV7VjfSecV/26TakgrUsnnfaK7T/OuQaVj7uND40sJyr0gG2CfRB0rdhDcnNpNsb8Necz78mAxhiAN4MgrvwB334f4vWGlTOqrCaTSVmh5ubWTcUSVCmG8IrvCRAjDjPWDJj33/tF4CNVwClYD4LvmPG0+/tOQOZUdwIsibEp1eMFXmlPdXyA70YrtHrafJBAQeGPfqOpfh1yoFRu3UebV05vlxsY8ZzWdiR6LxuJ5kYz+Y5ZXzprZfHikEJayNkrXguOuOsu1caBA1kFw/OWhNjYOYvYx3eWpZf+19Zj/wK1FSfmh+30L/EFCcZz0lutRCyRlKyp5+IqsLKazZH6t6BeinsYhP0bt4THnvfyEykdYe75Grf2oTe3uTSkHXBnKD3fDiLgXD5KBgf9x5di8o8D6CDD7vkkH5Eg54PClVlmftda3uL3K4MIl/nmjkgqsYEmspjBnfjQRWWNAn8+nCFuGDAX1xUe517VqWnZQyExNjP6ivXFKYV10K5lZY4J5wmQzl2wk20q4XIhFF3IeVlVpC5Es+V6f2e+rRG1GbdL0b/ETPHkItGcdeF7p1kHQE7JEeEnGcoap2SpBsgCYXLAvKwGXN5lO61rMjoc6gJZ75i8fyRVD2a7O8kDDTm25eZFwAmAwQQxKDhffatXXstjM+dRPMMa0Y6xNXCoecfUWZ/iaEJB0MQWSkYg3rWYfuhWibu3Vx/MP8AWrCmPEJqaLutyuc3tM85ZldqAnsujfLjRfHAHwAXWy8dwnL0=", new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"), "Admin", "827ccb0eea8a706c4c34a16891f84e7b", new Guid("a1972983-5389-49c5-a64d-bc4a65861a53") });

            migrationBuilder.CreateIndex(
                name: "IX_PolicySets_GroupId",
                table: "PolicySets",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySets_LoginId",
                table: "PolicySets",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySets_PolicyId",
                table: "PolicySets",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicySets");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "Policies");
        }
    }
}
