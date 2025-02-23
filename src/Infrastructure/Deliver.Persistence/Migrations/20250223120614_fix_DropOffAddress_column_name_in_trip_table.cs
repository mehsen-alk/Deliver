using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class fix_DropOffAddress_column_name_in_trip_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Addresses_DropOfAddressId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "DropOfAddressId",
                table: "Trips",
                newName: "DropOffAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_DropOfAddressId",
                table: "Trips",
                newName: "IX_Trips_DropOffAddressId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "77302539-c91f-48e7-867c-2c57853406b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b69c1cae-bdad-4fce-bb79-e7a1475e9648");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "881aed97-8b91-467c-ab48-c9f5054116b9", "AQAAAAIAAYagAAAAENeM2niD3sEiVRdF7kQVxz6ym3/0cEhsFmtUrAlAyY1mJXN8F1y/+tJZ2zh9RJz+Ug==", "60266ed3-3f5b-4fbc-8d2b-aa7c7f02a0ae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "088cd790-8ad9-45eb-aa62-0d0e7eb4384e", "AQAAAAIAAYagAAAAEMhzh/5s1rcJwayU7gPX088ic433tNQRxbM5PjhSepIADJBKeJ7Wj2JEWx0OkGKwaA==", "bb3d7860-7604-4bf7-a74d-79485bca47bb" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Addresses_DropOffAddressId",
                table: "Trips",
                column: "DropOffAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Addresses_DropOffAddressId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "DropOffAddressId",
                table: "Trips",
                newName: "DropOfAddressId");

            migrationBuilder.RenameIndex(
                name: "IX_Trips_DropOffAddressId",
                table: "Trips",
                newName: "IX_Trips_DropOfAddressId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2330cd41-ed54-4087-a2c5-e429e424d6bd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "a77c9db7-13de-409d-99d7-b7b1d26b8ec2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e0efb5a3-66c4-4862-88d9-4f363a3d71de", "AQAAAAIAAYagAAAAEOeGtCR4+XysiIcRKhRUDMmUeGcXs8KoIhUnNIrGe9rddLNQdgd9/OuAN1TH+c4n9g==", "cd9457c6-d798-4613-9161-93ff99458dc9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf5d05d9-b67f-4705-a70c-e61860f53fe2", "AQAAAAIAAYagAAAAEA9ISfF9M7O15gmAsZX4/c7gnH81AaPnkeqnNNXPF9t7A2jDo8oIepN2oJlmZl50ig==", "54df2e88-6dd1-41a3-81d3-603ce1604a7a" });

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Addresses_DropOfAddressId",
                table: "Trips",
                column: "DropOfAddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
