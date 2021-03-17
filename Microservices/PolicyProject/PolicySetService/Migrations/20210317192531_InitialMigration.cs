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
                table: "Logins",
                columns: new[] { "LoginId", "Certificate", "GroupId", "LogIn", "Password", "UserId" },
                values: new object[] { new Guid("fa3585fd-b259-4bb5-a37e-ce917fd13154"), "BwIAAACkAABSU0EyAAQAAAEAAQDBA9RfpDDf7vPUf760Zd3I2qknF+TBKmTkqPfLomG751XKApjRtNOG+3uvytkFocqjPoJOpEkuxR8YNBTqQ8GwpKfQGROeie3zNdgTwwyr+QptqJZICLBqMKV/C1O4rl7SNn8nSa8+iISviPa5mzV3Jip9WZWAy2NUISVk4VHotn80LWYup0rRd69i/mQX3iIy/lX2tCq53EKSePxLdu8zG1DdoafcnJtKroVgoD3CvS2mYuwrGNgBBjolHw6C+ua/p6WvJxGD0my9QTGtPsv6Ysg22ZpdWdYryAztq7LQFQyGKQHjfsJkPDtCxTFWeQb4PT6mN7YgI5sg2MzitLjKS0+L43eZI5iHcJ89cep6cXmU8KOimmjx08UFqLVshQs3STovQfxyafMSd6uDF1x8ZqhfHCJlcE3d/yPPqQkBVDlH+2VvAgYTzQU9ya5tLd9qQlulCpOJawjTLwn7ngd5pSz4PgDx9cC0ScarZR6Xl7slEHYk3o3HoE8m399ESIsj/qKKlvARGukw1roSrxY3I7RT6Hj5D1le0rh8Wr738vgNk3x8fnk1Xo6Q+HUeMI7fOLYFXTqYaD/4cWBHYbUtNQ0hdobO+dZHwhTDS6519oDb+b9kJPfQkOFL4kbEcFi3I0on2QdJVzBLMzDhAeziSy3OLg0PWKPQmTAZWfroVCs2X6GyRsn4BoLsW+Kfidghn1rpfrSnt68yvCMehXqmYojtWQxd/SDbSuR5D/Wc5F98DefQphY2lNOyFc8sRp8=", new Guid("b96c8d96-94d5-40df-bdc8-852b10199a4a"), "Admin", "827ccb0eea8a706c4c34a16891f84e7b", new Guid("a1972983-5389-49c5-a64d-bc4a65861a53") });

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
