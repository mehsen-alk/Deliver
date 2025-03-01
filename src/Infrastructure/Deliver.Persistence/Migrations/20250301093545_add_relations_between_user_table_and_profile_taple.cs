using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class add_relations_between_user_table_and_profile_taple : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_RidersProfile_UserId",
                table: "RidersProfile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DriversProfile_UserId",
                table: "DriversProfile",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriversProfile_AspNetUsers_UserId",
                table: "DriversProfile",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RidersProfile_AspNetUsers_UserId",
                table: "RidersProfile",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriversProfile_AspNetUsers_UserId",
                table: "DriversProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_RidersProfile_AspNetUsers_UserId",
                table: "RidersProfile");

            migrationBuilder.DropIndex(
                name: "IX_RidersProfile_UserId",
                table: "RidersProfile");

            migrationBuilder.DropIndex(
                name: "IX_DriversProfile_UserId",
                table: "DriversProfile");

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
        }
    }
}
