using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class changeDriverLocationcolumnnameinTipLogtabletoDriverLocationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DriverLocation",
                table: "TripLogs",
                newName: "DriverLocationId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cbaea4d4-0d07-42cb-9e65-0319801a5962");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b3c1409b-31e8-4e58-924f-04e78b7406f8");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d719b042-3e97-411f-a96f-8e751c520fd4", "AQAAAAIAAYagAAAAEISuDeW6iBFRkaD94PenBS30ynKwwMPROU8wtWIzqzPdcQm1J22LbXAuH3u49uHaLw==", "b22d5249-165f-4846-a84a-e3ebb2e03144" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "953af534-7712-4391-9273-1e4d6bb963cd", "AQAAAAIAAYagAAAAEM8k3V+Xsy8pJ5ePiYtyDRFKq1ANywJ2AFEc8K6AyB7Y3ol8ln48Or2FXIIopgkzLA==", "3a97a27a-9fa2-4f3b-8fda-bd4544977e07" });

            migrationBuilder.CreateIndex(
                name: "IX_TripLogs_DriverLocationId",
                table: "TripLogs",
                column: "DriverLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TripLogs_Addresses_DriverLocationId",
                table: "TripLogs",
                column: "DriverLocationId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripLogs_Addresses_DriverLocationId",
                table: "TripLogs");

            migrationBuilder.DropIndex(
                name: "IX_TripLogs_DriverLocationId",
                table: "TripLogs");

            migrationBuilder.RenameColumn(
                name: "DriverLocationId",
                table: "TripLogs",
                newName: "DriverLocation");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "f99afb8d-84ed-488e-99c3-2ca95217b509");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "4e7b7d68-d8e2-4687-b151-301654ccb176");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd4120f5-4eed-4104-b061-e94e542e3a12", "AQAAAAIAAYagAAAAEDOgtTBqINPQOXbEjsC2SNg30JDby6ROw+16S2gtueDxWiIYm1lHO7y6P7v3QjOVYw==", "eec2627f-dfda-4c25-9aed-f1c1136a3c31" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ea0b094e-4316-42c4-96c1-d36058995c0f", "AQAAAAIAAYagAAAAENOIrPznzmLBmFJzG7jW9AEjw/D7akdITNi64cUmJKZGXYp7Jn1CpVUwzCzPf176/A==", "a2028685-2098-4e6e-b518-522b265c5d29" });
        }
    }
}
