using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ImprovePrfilesTablesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverProfiles_Addresses_CurrentLocationId",
                table: "DriverProfiles");

            migrationBuilder.DropTable(
                name: "ClientProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverProfiles",
                table: "DriverProfiles");

            migrationBuilder.RenameTable(
                name: "DriverProfiles",
                newName: "DriversProfile");

            migrationBuilder.RenameIndex(
                name: "IX_DriverProfiles_CurrentLocationId",
                table: "DriversProfile",
                newName: "IX_DriversProfile_CurrentLocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriversProfile",
                table: "DriversProfile",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RidersProfile",
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
                    table.PrimaryKey("PK_RidersProfile", x => x.Id);
                });

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

            migrationBuilder.InsertData(
                table: "RidersProfile",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "LastModifiedBy", "LastModifiedDate", "Name", "Phone", "ProfileImage", "Status", "UserId" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Mohsen", "221234", "https://c4d-media.s3.eu-central-1.amazonaws.com/upload/image/original-image/2023-01-29_20-32-24/scaled-image-picker8315132317025791363-63de775a3982c.jpg", 0, 1 });

            migrationBuilder.AddForeignKey(
                name: "FK_DriversProfile_Addresses_CurrentLocationId",
                table: "DriversProfile",
                column: "CurrentLocationId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriversProfile_Addresses_CurrentLocationId",
                table: "DriversProfile");

            migrationBuilder.DropTable(
                name: "RidersProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriversProfile",
                table: "DriversProfile");

            migrationBuilder.RenameTable(
                name: "DriversProfile",
                newName: "DriverProfiles");

            migrationBuilder.RenameIndex(
                name: "IX_DriversProfile_CurrentLocationId",
                table: "DriverProfiles",
                newName: "IX_DriverProfiles_CurrentLocationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverProfiles",
                table: "DriverProfiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ClientProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProfiles", x => x.Id);
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

            migrationBuilder.AddForeignKey(
                name: "FK_DriverProfiles_Addresses_CurrentLocationId",
                table: "DriverProfiles",
                column: "CurrentLocationId",
                principalTable: "Addresses",
                principalColumn: "Id");
        }
    }
}
