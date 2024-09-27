using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Deliver.Identity.Migrations
{
    /// <inheritdoc />
    public partial class addNameToAspNetUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "00e72f45-1110-4db6-88ef-a0395d8a4750", "Mohsen", "AQAAAAIAAYagAAAAECzc1yJgfiFIblv4LKA0k/FKpt3HD6hehzsNUJeZHDRlIo2OEbwiTOf7zVS6GbWdEg==", "221234", "77eee7af-68fd-4f2f-a3f3-505daad5a494" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "82dff83f-77ec-460e-b8c2-6ce1159929d3", "Mohammed", "AQAAAAIAAYagAAAAEH/wXWsPwQSElU5NPzNgckPfs2kyehu6YqlbbPydRqL582PNXBOt5N7435EFjy4NiA==", "331234", "8cf9033c-2ec9-4f92-a0a7-56b5e1d00d19" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "1657da87-249c-42ef-bcba-e83daeee3d02");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e508e21f-20db-4f43-a681-88cf2f6ef185");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "65cead37-18ed-4b10-9f8d-e892308f8854", "AQAAAAIAAYagAAAAEGgrY0KSIVPMkpBlbbRMeipRXtFdMZDvy1606Ttzi5aiOElFIHVjcPdf+xj+3PruHA==", null, "07f3153a-08be-4b2f-b4d6-139db2715b7b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "PhoneNumber", "SecurityStamp" },
                values: new object[] { "426a53b1-7bc7-4ca9-9154-679c768bdfe9", "AQAAAAIAAYagAAAAEKWVcEvIPbH70PbmgRHgx3kahTa6y38VlEE7j9EkSkOQVxOhw9LhFMqU4UpKzeIeBQ==", null, "075a3ada-f04a-413a-9872-86d181f600fd" });
        }
    }
}
