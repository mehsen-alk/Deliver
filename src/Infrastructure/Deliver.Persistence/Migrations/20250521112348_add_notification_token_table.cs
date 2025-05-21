using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_notification_token_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DeviceId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastUseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8652a290-4b4b-4ee5-ab01-79adfd0d08f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e10c787a-8bcd-4014-aaa8-59fd8b832c25");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b0f85270-830c-4f3f-9f37-368f93432e6d", "AQAAAAIAAYagAAAAEEY02iS8JMUAOBeTvzKkHRpSiP/g/oNqO/dg43oy0LfJKHMyJhSjlMCG+qgm9Oo3lw==", "c36147e2-728d-4576-9812-f71cdc84c854" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a73ae04d-2624-4005-8e10-1ff3887d2653", "AQAAAAIAAYagAAAAEFmqi5hWLdD/FGk7Yh29PTmOR3Eq+D96hTO6X/ItNve5Sl1NTR/27TxBdCU99S0Ncg==", "a19696a1-2857-4810-a50f-cfd206fd9ba2" });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTokens_Token",
                table: "NotificationTokens",
                column: "Token",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotificationTokens_UserId_DeviceId",
                table: "NotificationTokens",
                columns: new[] { "UserId", "DeviceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationTokens");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "69eed2f0-70bf-4b31-8282-b15fd6ad47bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "2de7d538-7d0a-48f5-a217-b726c7d3115f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76d53352-7584-40a4-bdc2-8b96d4e91825", "AQAAAAIAAYagAAAAEO9OSqMLWl029EXRh2sob0yktAdfr82zUUXf+crpjAUjj7lrOVayFkEnz71yf/XfWQ==", "e9fc5884-adcc-4026-998d-4afd132d6981" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b61c9953-b27b-4a9d-8a14-179ef576623e", "AQAAAAIAAYagAAAAEPgKFd7ky0e4EJfNc5VCwiE7d75P3cFm/2mysi8utvCZ61MMnGlayNy5JI2zOyi8AA==", "158ef5dc-f627-46bb-9674-75b93d7eeb7d" });
        }
    }
}
