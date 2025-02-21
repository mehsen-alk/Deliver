using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addusersprofileentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ClientProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DriverProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentLocationId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverProfiles_Addresses_CurrentLocationId",
                        column: x => x.CurrentLocationId,
                        principalTable: "Addresses",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2b013088-857d-4115-bff2-884fea6bda45");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "cb47a30d-b68f-4dba-886f-dd607e773d8b");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce70ba53-e9dc-4806-8352-26fa3083a2b0", "AQAAAAIAAYagAAAAEH35erDtcN/NJLWEeKOt5mf2lcD1PQjX6qcRsqeX6cejVnoWy/KgeO09X8KIPYqcZQ==", "939b0ae5-9aa4-458d-b80b-682e48e227b5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bf79f137-7da3-4039-9237-2df2467034ad", "AQAAAAIAAYagAAAAEIlAygrPgvxG5Rj9PsLd88MDAVBJqKDtwrbXKZS27QHEfuV20jS/amgvbF8TRLE+pg==", "05759366-01d3-4aa0-81b6-809209a68667" });

            migrationBuilder.InsertData(
                table: "ClientProfiles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Name", "Phone", "ProfileImage", "Status", "UserId" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Mohsen", "221234", "https://c4d-media.s3.eu-central-1.amazonaws.com/upload/image/original-image/2023-01-29_20-32-24/scaled-image-picker8315132317025791363-63de775a3982c.jpg", 0, 1 });

            migrationBuilder.InsertData(
                table: "DriverProfiles",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "CurrentLocationId", "LastModifiedBy", "LastModifiedDate", "LicenseImage", "Name", "Phone", "ProfileImage", "Status", "UserId", "VehicleImage" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "https://c4d-media.s3.eu-central-1.amazonaws.com/upload/image/original-image/2023-01-29_20-32-24/scaled-image-picker8315132317025791363-63de775a3982c.jpg", "Mohammed", "221234", "https://c4d-media.s3.eu-central-1.amazonaws.com/upload/image/original-image/2023-01-29_20-32-24/scaled-image-picker8315132317025791363-63de775a3982c.jpg", 0, 2, "https://c4d-media.s3.eu-central-1.amazonaws.com/upload/image/original-image/2023-01-29_20-32-24/scaled-image-picker8315132317025791363-63de775a3982c.jpg" });

            migrationBuilder.CreateIndex(
                name: "IX_DriverProfiles_CurrentLocationId",
                table: "DriverProfiles",
                column: "CurrentLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientProfiles");

            migrationBuilder.DropTable(
                name: "DriverProfiles");

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
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6df0f485-1ea4-4ac3-b0a0-f6137e655cc9", "Mohsen", "AQAAAAIAAYagAAAAEH8KiuWWBhvENu+FLrXh06BjHCbcJtzXv8PiOUZD5LM9JOUXHVxub7AiD0mB3p9urQ==", "f0cd8313-b759-4926-b997-cb5e0138a864" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "Name", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce39d2d6-52e9-4cff-a1ef-f9e29c47e678", "Mohammed", "AQAAAAIAAYagAAAAEPobOfEQQBMcC14a9y9MbszWwWu38bX61+FDx0iXwn2sAGoz6xe5owDQlGq7eIsuoQ==", "2c2e5a74-ae12-4535-95cf-1a0d80fda2ec" });
        }
    }
}
