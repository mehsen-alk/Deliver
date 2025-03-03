using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_payments_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TripId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CompanyCommission = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    PaymentGatewayId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromUserId = table.Column<int>(type: "int", nullable: false),
                    ToUserId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_FromUserId",
                        column: x => x.FromUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_AspNetUsers_ToUserId",
                        column: x => x.ToUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Payments_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Payments_FromUserId",
                table: "Payments",
                column: "FromUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ToUserId",
                table: "Payments",
                column: "ToUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_TripId",
                table: "Payments",
                column: "TripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "c4130628-ac12-43d7-b6dd-9786d6f5e726");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "9f2a0604-7765-4ed4-9944-27cbe5182cd1");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "316179be-4dfd-43c9-a1da-f7f27cdb2616", "AQAAAAIAAYagAAAAEG26wKzGbpa8hcrhT9zjXW0YQyw67J5D0MN862o52DLbpNQ7f6j/kyqXea2Ie0dLzA==", "f795e346-bc45-4ed5-9d34-0b56ce8d1731" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35138430-6f36-4f48-9b3a-c9d0589853bb", "AQAAAAIAAYagAAAAEGFMQifevTiafLQQrZzTKIwlYhnNE9AQfVa2ZcZcEaSV52b0O+lVg06vIt2xgpVpCA==", "0e35ae9f-d9a0-4824-b0ab-7d1411a2cf26" });
        }
    }
}
