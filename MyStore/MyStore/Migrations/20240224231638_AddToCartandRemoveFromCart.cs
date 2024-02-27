using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyStore.Migrations
{
    /// <inheritdoc />
    public partial class AddToCartandRemoveFromCart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9dac06b4-c4d3-43d2-a8b0-6c143ef9c897");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5a3bd97-439a-479c-8f56-7a19ed672a8e");

            migrationBuilder.AddColumn<bool>(
                name: "InCart",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "457982ce-e18d-48ad-bc17-4f22b8f24490", null, "admin", "ADMIN" },
                    { "5f36a080-7f75-4e98-a2a2-db0e4415e1c5", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "457982ce-e18d-48ad-bc17-4f22b8f24490");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5f36a080-7f75-4e98-a2a2-db0e4415e1c5");

            migrationBuilder.DropColumn(
                name: "InCart",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9dac06b4-c4d3-43d2-a8b0-6c143ef9c897", null, "admin", "ADMIN" },
                    { "b5a3bd97-439a-479c-8f56-7a19ed672a8e", null, "customer", "CUSTOMER" }
                });
        }
    }
}
