using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class FilleNameMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13216294-ebbd-44d7-ab22-644a272c5dcd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0a450df-5645-4450-9be0-64ada5515c6c");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0582c05e-3f75-40df-92f4-9bceab1e9254", null, "User", "USER" },
                    { "1809714f-682b-46bf-a3a0-e36949905fe2", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0582c05e-3f75-40df-92f4-9bceab1e9254");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1809714f-682b-46bf-a3a0-e36949905fe2");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13216294-ebbd-44d7-ab22-644a272c5dcd", null, "User", "USER" },
                    { "f0a450df-5645-4450-9be0-64ada5515c6c", null, "Admin", "ADMIN" }
                });
        }
    }
}
