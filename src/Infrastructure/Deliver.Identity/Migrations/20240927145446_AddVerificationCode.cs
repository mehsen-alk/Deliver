using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliver.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddVerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VerificationCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationCodes_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "fc0c504a-8500-408b-a474-60566296bc68");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a1da9944-b724-47e7-a09f-47803d34b169");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "622474f0-011d-4fd7-86dc-3b2ebb1d74a6", "AQAAAAIAAYagAAAAENcZFEoH/sPlmBxoYPP+RuRuvSlEnB84s70Far/lHR2fG7CmMmDeApkwpcqsMEzsKQ==", "6c4a6e5c-7a20-4036-aeee-6e4fc0262147" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c9b0a62-e898-4701-8bb6-bc2db0be0ecb", "AQAAAAIAAYagAAAAEEviMGuY5au1NMIPh+H+9pos2dbg0vYfvzRGfviyqNr+QOWA3k4wKgZ0sPdWQGZFSw==", "7ff39a2b-de52-458f-acad-62637eaaca17" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationCodes");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "11722365-a5d2-4572-9f3e-1b723c85c96d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3cc05e62-c3d0-403c-a52b-637eb5c332d2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "00e72f45-1110-4db6-88ef-a0395d8a4750", "AQAAAAIAAYagAAAAECzc1yJgfiFIblv4LKA0k/FKpt3HD6hehzsNUJeZHDRlIo2OEbwiTOf7zVS6GbWdEg==", "77eee7af-68fd-4f2f-a3f3-505daad5a494" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "82dff83f-77ec-460e-b8c2-6ce1159929d3", "AQAAAAIAAYagAAAAEH/wXWsPwQSElU5NPzNgckPfs2kyehu6YqlbbPydRqL582PNXBOt5N7435EFjy4NiA==", "8cf9033c-2ec9-4f92-a0a7-56b5e1d00d19" });
        }
    }
}
