using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class SevMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "283fd566-14b0-4fe4-97ed-9f17e5b1dcce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5877ac65-4d89-453c-85a5-fc7629d20081");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "415effe4-7cb8-4d29-98ea-513968120b33", null, "User", "USER" },
                    { "bbfeb8dc-0f8e-4f53-9a58-502f16a923ae", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "415effe4-7cb8-4d29-98ea-513968120b33");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbfeb8dc-0f8e-4f53-9a58-502f16a923ae");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "283fd566-14b0-4fe4-97ed-9f17e5b1dcce", null, "User", "USER" },
                    { "5877ac65-4d89-453c-85a5-fc7629d20081", null, "Admin", "ADMIN" }
                });
        }
    }
}
