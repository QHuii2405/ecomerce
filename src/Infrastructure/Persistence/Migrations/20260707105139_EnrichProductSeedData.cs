using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnrichProductSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "IsDeleted", "Name", "ParentId", "UpdatedAt" },
                values: new object[] { new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Phụ kiện Điện thoại & Sạc", false, "Accessories", null, null });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "AttributesJson", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "ProductId", "ReservedQuantity", "Sku", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000007"), "{\"switch\":\"GL Tactile\",\"color\":\"Black\",\"connection\":\"Lightspeed Wireless\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "GL Tactile / Black", 3790000m, new Guid("d0000000-0000-0000-0000-000000000005"), 0, "LOGI-G915-TKL", 30, null },
                    { new Guid("a0000000-0000-0000-0000-000000000008"), "{\"switch\":\"Razer Green\",\"color\":\"Black\",\"connection\":\"Wired\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Green Switch / Black", 5990000m, new Guid("d0000000-0000-0000-0000-000000000006"), 0, "RAZER-BW-V4", 25, null },
                    { new Guid("a0000000-0000-0000-0000-000000000009"), "{\"color\":\"Black\",\"connection\":\"Bluetooth 5.2\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Standard Edition", 14490000m, new Guid("d0000000-0000-0000-0000-000000000007"), 0, "SONY-XV800", 10, null },
                    { new Guid("a0000000-0000-0000-0000-000000000010"), "{\"color\":\"Black\",\"battery\":\"20 Hours\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Black", 3490000m, new Guid("d0000000-0000-0000-0000-000000000008"), 0, "JBL-CHG5-BLK", 50, null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "ImageUrls",
                value: "[\"/image/latop/images (1).jpg\",\"/image/latop/images (2).jpg\",\"/image/latop/images (3).jpg\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                column: "ImageUrls",
                value: "[\"/image/latop/images (4).jpg\",\"/image/latop/images (5).jpg\",\"/image/latop/images (6).jpg\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "ImageUrls",
                value: "[\"/image/phone/images (1).jpg\",\"/image/phone/images (2).jpg\",\"/image/phone/images (3).jpg\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                column: "ImageUrls",
                value: "[\"/image/phone/images (4).jpg\",\"/image/phone/images (5).jpg\",\"/image/phone/images (6).jpg\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                columns: new[] { "Description", "ImageUrls", "Name" },
                values: new object[] { "Bàn phím cơ không dây siêu mỏng, trang bị switch GL Low Profile cho tốc độ phản hồi cực nhanh. Hệ thống LED RGB LIGHTSYNC rực rỡ và thời lượng pin cực khủng.", "[\"/image/keyboard/images (1).jpg\",\"/image/keyboard/images (2).jpg\"]", "Logitech G915 TKL Wireless" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                column: "ImageUrls",
                value: "[\"/image/keyboard/images (3).jpg\",\"/image/keyboard/images (4).jpg\"]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"),
                columns: new[] { "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Loa Bluetooth không dây công suất lớn với âm thanh 360 độ cực đỉnh. Tích hợp đèn LED đa sắc và cổng cắm micro hát karaoke chuyên nghiệp.", "[\"/image/speaker/images (1).jpg\",\"/image/speaker/images (2).jpg\"]", "Sony SRS-XV800 Wireless", 14490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"),
                columns: new[] { "Brand", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "JBL", "Loa di động chống nước chuẩn IP67 với âm thanh Original Pro Sound cực mạnh. Thời lượng pin 20 tiếng, tích hợp sạc dự phòng vô cùng tiện lợi.", "[\"/image/speaker/images (3).jpg\",\"/image/speaker/images (4).jpg\"]", "JBL Charge 5", 3490000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AttributesJson", "Brand", "CategoryId", "CreatedAt", "Description", "ImageUrls", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000009"), "{}", "Anker", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Củ sạc siêu nhanh 65W với công nghệ GaN thế hệ mới. Trang bị 2 cổng USB-C và 1 cổng USB-A, thiết kế siêu nhỏ gọn và an toàn tuyệt đối.", "[\"/image/charger/images (2).jpg\",\"/image/charger/images (3).jpg\"]", false, "Anker 735 Charger (GaNPrime 65W)", 1290000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000010"), "{}", "Spigen", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Ốp lưng siêu chống sốc với công nghệ Air Cushion độc quyền. Thiết kế 2 lớp bảo vệ cực tốt kèm chân chống tiện lợi cho việc xem video.", "[\"/image/phone case/images (1).jpg\",\"/image/phone case/images (2).jpg\"]", false, "Spigen Tough Armor Case iPhone 15", 850000m, null }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ProductId", "ReservedQuantity", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("e0000000-0000-0000-0000-000000000009"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000009"), 0, 200, null },
                    { new Guid("e0000000-0000-0000-0000-000000000010"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000010"), 0, 150, null }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "AttributesJson", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "ProductId", "ReservedQuantity", "Sku", "StockQuantity", "UpdatedAt" },
                values: new object[] { new Guid("a0000000-0000-0000-0000-000000000011"), "{\"color\":\"Black\",\"power\":\"65W\",\"ports\":\"2C 1A\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "65W / Black", 1290000m, new Guid("d0000000-0000-0000-0000-000000000009"), 0, "ANKER-735-BLK", 100, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "ImageUrls",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                column: "ImageUrls",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "ImageUrls",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                column: "ImageUrls",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                columns: new[] { "Description", "ImageUrls", "Name" },
                values: new object[] { "Chuột gaming siêu nhẹ chỉ 60g, trang bị switch lai quang học-cơ học LIGHTFORCE và cảm biến HERO 2 cao cấp. Dành riêng cho tuyển thủ eSports.", "[]", "Logitech G Pro X Superlight 2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                column: "ImageUrls",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"),
                columns: new[] { "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Tai nghe Over-ear chống ồn chủ động (ANC) tốt nhất thế giới. Tích hợp 8 micro, màng loa 30mm tinh chỉnh âm thanh High-Resolution Audio.", "[]", "Sony WH-1000XM5", 7490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"),
                columns: new[] { "Brand", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Apple", "Tai nghe In-ear True Wireless chống ồn đỉnh cao. Chip H2 xử lý âm thanh trong trẻo, chế độ Xuyên âm thông minh, cổng sạc Type-C thế hệ mới.", "[]", "AirPods Pro 2 (USB-C)", 6190000m });
        }
    }
}
