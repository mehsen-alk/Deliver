using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addnewcolumntoTripLogtabletosavethelogtype : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "TripLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "89c5d5a4-0e7a-4ba3-82d9-d17e0c100580");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "11567e96-7eb7-4290-80d4-3344df331051");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6df0f485-1ea4-4ac3-b0a0-f6137e655cc9", "AQAAAAIAAYagAAAAEH8KiuWWBhvENu+FLrXh06BjHCbcJtzXv8PiOUZD5LM9JOUXHVxub7AiD0mB3p9urQ==", "f0cd8313-b759-4926-b997-cb5e0138a864" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce39d2d6-52e9-4cff-a1ef-f9e29c47e678", "AQAAAAIAAYagAAAAEPobOfEQQBMcC14a9y9MbszWwWu38bX61+FDx0iXwn2sAGoz6xe5owDQlGq7eIsuoQ==", "2c2e5a74-ae12-4535-95cf-1a0d80fda2ec" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "TripLogs");

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
        }
    }
}
