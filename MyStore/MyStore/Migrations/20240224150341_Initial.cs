using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyStore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6aff2cec-02f5-4565-bef2-04c94a865491");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb392ccc-5558-4886-8184-6e989d8a1ee2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a21e80da-7d4b-4cd0-926a-8f9d565dd1c4", null, "admin", "ADMIN" },
                    { "cc491f77-fece-4b54-80dd-bd46fd873efb", null, "customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a21e80da-7d4b-4cd0-926a-8f9d565dd1c4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cc491f77-fece-4b54-80dd-bd46fd873efb");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6aff2cec-02f5-4565-bef2-04c94a865491", null, "customer", "CUSTOMER" },
                    { "fb392ccc-5558-4886-8184-6e989d8a1ee2", null, "admin", "ADMIN" }
                });
        }
    }
}
