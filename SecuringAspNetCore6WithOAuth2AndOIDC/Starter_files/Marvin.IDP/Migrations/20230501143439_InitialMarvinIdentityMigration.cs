using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Marvin.IDP.Migrations
{
    public partial class InitialMarvinIdentityMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Subject = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    Value = table.Column<string>(type: "TEXT", maxLength: 250, nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "UserName" },
                values: new object[] { new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), true, "bfa4963d-1657-43c4-b588-968626b69e01", "password", "d860efca-22d9-47fd-8249-791ba61b07c7", "David" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Active", "ConcurrencyStamp", "Password", "Subject", "UserName" },
                values: new object[] { new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), true, "bc529996-3c27-4b32-9815-f2499bda3a22", "password", "b7539694-97e7-4dfe-84da-b4256e1ff5c7", "Emma" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("085af635-9465-4b5a-9036-3662da5d66cb"), "3fc2444e-25e8-4356-bbb0-9ea6e15a0c73", "country", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "nl" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("1b92c5bf-8518-4c13-9e9a-ef02e55dd9ef"), "6610f2d9-99a8-45d0-b4f3-49977a7e176d", "family_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "Flagg" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("83f2cf61-69da-4c11-814a-136605cfa7e4"), "c9b3b2d2-2930-44ca-bd25-feba1eb7a346", "role", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "PayingUser" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("858bca08-caeb-4284-a365-30cfc4971e1d"), "b08c73b2-216e-4893-badc-b5e854e1c50a", "role", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "FreeUser" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("8ed602f9-b87f-49b3-a101-55c4b2ed009c"), "2bdca94e-9e37-495c-bbf1-217bb874f844", "given_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Emma" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("c149ca5d-08e8-4bff-ae6a-f77cbd0387df"), "702ead8f-1ad7-489e-9c2b-c7909bf3ebc8", "family_name", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "Flagg" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("e32a7f22-9838-493a-b190-43471aba4b7a"), "2cd002f0-7a65-4f66-8ca3-f68b790fb4d8", "country", new Guid("96053525-f4a5-47ee-855e-0ea77fa6c55a"), "be" });

            migrationBuilder.InsertData(
                table: "UserClaims",
                columns: new[] { "Id", "ConcurrencyStamp", "Type", "UserId", "Value" },
                values: new object[] { new Guid("ead8ba99-c0d4-413a-8d8c-c6b9b2c8666a"), "5138c8cc-f70e-46e9-b4e7-093c42236a48", "given_name", new Guid("13229d33-99e0-41b3-b18d-4f72127e3971"), "David" });

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Subject",
                table: "Users",
                column: "Subject",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
