using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedLuminaProductsFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "ParentId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Laptops and PCs", false, "Laptops", null, null },
                    { new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Gaming Gear & Peripherals", false, "Gaming", null, null },
                    { new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Premium Audio Equipment", false, "Audio", null, null },
                    { new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Smartphones and Mobile devices", false, "Smartphones", null, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c71"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Sức mạnh tối thượng từ vi xử lý Intel Core Ultra mới nhất, thiết kế siêu mỏng nhẹ sang trọng.", false, "Lumina Pro X1", 32490000m, null },
                    { new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c72"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Bàn phím cơ không dây đỉnh cao, switch êm ái, đèn nền RGB cá tính.", false, "Ghost Mechanical Keyboard", 4725000m, null },
                    { new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c73"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Tai nghe chống ồn chủ động vượt trội, âm thanh độ phân giải cao sắc nét.", false, "Sonic Buds Pro", 6225000m, null },
                    { new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c74"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đồng hồ thông minh thế hệ mới tích hợp đo chỉ số sức khỏe chuyên sâu.", false, "Nexus Watch S", 9975000m, null }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ProductId", "ReservedQuantity", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c81"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c71"), 0, 10, null },
                    { new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c82"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c72"), 0, 25, null },
                    { new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c83"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c73"), 0, 50, null },
                    { new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c84"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c74"), 0, 15, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c81"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c82"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c83"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c84"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c71"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c72"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c73"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c74"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"));
        }
    }
}
