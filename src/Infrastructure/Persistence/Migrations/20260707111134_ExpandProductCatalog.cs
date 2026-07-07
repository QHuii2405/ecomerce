using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ExpandProductCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "ProductReviews",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000001"),
                column: "StockQuantity",
                value: 39);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000002"),
                column: "StockQuantity",
                value: 59);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000003"),
                column: "StockQuantity",
                value: 33);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000004"),
                column: "StockQuantity",
                value: 31);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000005"),
                column: "StockQuantity",
                value: 58);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000006"),
                column: "StockQuantity",
                value: 52);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000007"),
                column: "StockQuantity",
                value: 18);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000008"),
                column: "StockQuantity",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000009"),
                column: "StockQuantity",
                value: 36);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000010"),
                column: "StockQuantity",
                value: 47);

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\"}", "Phiên bản Tiêu chuẩn", 20600000m, "APP-1-0", 5 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"White\"}", "Phiên bản Cao cấp", 21600000m, "APP-1-1", 15 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\"}", "Phiên bản Tiêu chuẩn", 10500000m, "ASU-2-0", 23 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\"}", "Phiên bản Tiêu chuẩn", 8000000m, "DEL-3-0", 7 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"White\"}", "Phiên bản Cao cấp", 9000000m, "DEL-3-1", 11 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\"}", "Phiên bản Tiêu chuẩn", 2700000m, "HP-4-0", 17 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\"}", "Phiên bản Tiêu chuẩn", 11600000m, "LEN-5-0", 23 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000008"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"White\"}", "Phiên bản Cao cấp", 12600000m, new Guid("d0000000-0000-0000-0000-000000000005"), "LEN-5-1", 17 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000009"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\"}", "Phiên bản Tiêu chuẩn", 12500000m, new Guid("d0000000-0000-0000-0000-000000000006"), "APP-6-0", 23 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000010"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\"}", "Phiên bản Tiêu chuẩn", 1000000m, new Guid("d0000000-0000-0000-0000-000000000007"), "ASU-7-0", 12 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000011"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"White\"}", "Phiên bản Cao cấp", 2000000m, new Guid("d0000000-0000-0000-0000-000000000007"), "ASU-7-1", 8 });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "AttributesJson", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "ProductId", "ReservedQuantity", "Sku", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000012"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 20900000m, new Guid("d0000000-0000-0000-0000-000000000008"), 0, "DEL-8-0", 10, null },
                    { new Guid("a0000000-0000-0000-0000-000000000013"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 12900000m, new Guid("d0000000-0000-0000-0000-000000000009"), 0, "HP-9-0", 20, null },
                    { new Guid("a0000000-0000-0000-0000-000000000014"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 13900000m, new Guid("d0000000-0000-0000-0000-000000000009"), 0, "HP-9-1", 21, null },
                    { new Guid("a0000000-0000-0000-0000-000000000015"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 7300000m, new Guid("d0000000-0000-0000-0000-000000000010"), 0, "LEN-10-0", 16, null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                columns: new[] { "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Đây là sản phẩm Laptops cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (1).jpg\",\"/image/latop/images (10).jpg\"]", "Apple Premium Laptops Model 1", 20600000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                columns: new[] { "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Đây là sản phẩm Laptops cao cấp từ thương hiệu Asus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (11).jpg\",\"/image/latop/images (12).jpg\"]", "Asus Premium Laptops Model 2", 10500000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Dell", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu Dell. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (13).jpg\",\"/image/latop/images (14).jpg\"]", "Dell Premium Laptops Model 3", 8000000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "HP", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu HP. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (15).jpg\",\"/image/latop/images (16).jpg\"]", "HP Premium Laptops Model 4", 2700000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Lenovo", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu Lenovo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (17).jpg\",\"/image/latop/images (2).jpg\"]", "Lenovo Premium Laptops Model 5", 11600000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Apple", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (3).jpg\",\"/image/latop/images (4).jpg\"]", "Apple Premium Laptops Model 6", 12500000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Asus", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu Asus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (5).jpg\",\"/image/latop/images (6).jpg\"]", "Asus Premium Laptops Model 7", 1000000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Dell", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu Dell. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (7).jpg\",\"/image/latop/images (8).jpg\"]", "Dell Premium Laptops Model 8", 20900000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "HP", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu HP. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (9).jpg\",\"/image/latop/images.jpg\"]", "HP Premium Laptops Model 9", 12900000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000010"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Lenovo", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), "Đây là sản phẩm Laptops cao cấp từ thương hiệu Lenovo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/latop/images (1).jpg\",\"/image/latop/images (10).jpg\"]", "Lenovo Premium Laptops Model 10", 7300000m });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "AttributesJson", "Brand", "CategoryId", "CreatedAt", "Description", "ImageUrls", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000011"), "{}", "Apple", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (1).jpg\",\"/image/phone/images (10).jpg\"]", false, "Apple Premium Smartphones Model 1", 4600000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000012"), "{}", "Samsung", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Samsung. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (11).jpg\",\"/image/phone/images (12).jpg\"]", false, "Samsung Premium Smartphones Model 2", 15800000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000013"), "{}", "Xiaomi", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Xiaomi. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (13).jpg\",\"/image/phone/images (14).jpg\"]", false, "Xiaomi Premium Smartphones Model 3", 12500000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000014"), "{}", "Oppo", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Oppo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (15).jpg\",\"/image/phone/images (16).jpg\"]", false, "Oppo Premium Smartphones Model 4", 17200000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000015"), "{}", "Vivo", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Vivo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (18).jpg\",\"/image/phone/images (2).jpg\"]", false, "Vivo Premium Smartphones Model 5", 17700000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000016"), "{}", "Apple", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (3).jpg\",\"/image/phone/images (4).jpg\"]", false, "Apple Premium Smartphones Model 6", 1300000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000017"), "{}", "Samsung", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Samsung. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (5).jpg\",\"/image/phone/images (6).jpg\"]", false, "Samsung Premium Smartphones Model 7", 9700000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000018"), "{}", "Xiaomi", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Xiaomi. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (7).jpg\",\"/image/phone/images (8).jpg\"]", false, "Xiaomi Premium Smartphones Model 8", 11100000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000019"), "{}", "Oppo", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Oppo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (9).jpg\",\"/image/phone/images.jpg\"]", false, "Oppo Premium Smartphones Model 9", 12300000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000020"), "{}", "Vivo", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Vivo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone/images (1).jpg\",\"/image/phone/images (10).jpg\"]", false, "Vivo Premium Smartphones Model 10", 6800000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000021"), "{}", "Logitech", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu Logitech. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (1).jpg\",\"/image/keyboard/images (10).jpg\"]", false, "Logitech Premium Gaming Model 1", 13200000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000022"), "{}", "Razer", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu Razer. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (11).jpg\",\"/image/keyboard/images (12).jpg\"]", false, "Razer Premium Gaming Model 2", 12100000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000023"), "{}", "Corsair", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu Corsair. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (13).jpg\",\"/image/keyboard/images (14).jpg\"]", false, "Corsair Premium Gaming Model 3", 14600000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000024"), "{}", "SteelSeries", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu SteelSeries. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (15).jpg\",\"/image/keyboard/images (16).jpg\"]", false, "SteelSeries Premium Gaming Model 4", 17000000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000025"), "{}", "HyperX", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu HyperX. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (17).jpg\",\"/image/keyboard/images (18).jpg\"]", false, "HyperX Premium Gaming Model 5", 1400000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000026"), "{}", "Logitech", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu Logitech. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (2).jpg\",\"/image/keyboard/images (3).jpg\"]", false, "Logitech Premium Gaming Model 6", 17000000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000027"), "{}", "Razer", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu Razer. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (4).jpg\",\"/image/keyboard/images (5).jpg\"]", false, "Razer Premium Gaming Model 7", 8300000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000028"), "{}", "Corsair", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu Corsair. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (6).jpg\",\"/image/keyboard/images (7).jpg\"]", false, "Corsair Premium Gaming Model 8", 17400000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000029"), "{}", "SteelSeries", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu SteelSeries. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images (8).jpg\",\"/image/keyboard/images (9).jpg\"]", false, "SteelSeries Premium Gaming Model 9", 16400000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000030"), "{}", "HyperX", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Gaming cao cấp từ thương hiệu HyperX. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/keyboard/images.jpg\",\"/image/keyboard/t\\u1EA3i xu\\u1ED1ng (1).webp\"]", false, "HyperX Premium Gaming Model 10", 15400000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000031"), "{}", "Sony", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Sony. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (1).jpg\",\"/image/speaker/images (10).jpg\"]", false, "Sony Premium Audio Model 1", 9400000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000032"), "{}", "JBL", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu JBL. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (11).jpg\",\"/image/speaker/images (12).jpg\"]", false, "JBL Premium Audio Model 2", 9500000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000033"), "{}", "Bose", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Bose. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (13).jpg\",\"/image/speaker/images (14).jpg\"]", false, "Bose Premium Audio Model 3", 14100000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000034"), "{}", "Marshall", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Marshall. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (15).jpg\",\"/image/speaker/images (16).jpg\"]", false, "Marshall Premium Audio Model 4", 16700000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000035"), "{}", "Sennheiser", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Sennheiser. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (19).jpg\",\"/image/speaker/images (2).jpg\"]", false, "Sennheiser Premium Audio Model 5", 1600000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000036"), "{}", "Sony", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Sony. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (3).jpg\",\"/image/speaker/images (4).jpg\"]", false, "Sony Premium Audio Model 6", 10300000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000037"), "{}", "JBL", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu JBL. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (5).jpg\",\"/image/speaker/images (6).jpg\"]", false, "JBL Premium Audio Model 7", 10400000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000038"), "{}", "Bose", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Bose. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (7).jpg\",\"/image/speaker/images (8).jpg\"]", false, "Bose Premium Audio Model 8", 17800000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000039"), "{}", "Marshall", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Marshall. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (9).jpg\",\"/image/speaker/images.jpg\"]", false, "Marshall Premium Audio Model 9", 2700000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000040"), "{}", "Sennheiser", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Audio cao cấp từ thương hiệu Sennheiser. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/speaker/images (1).jpg\",\"/image/speaker/images (10).jpg\"]", false, "Sennheiser Premium Audio Model 10", 7600000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000041"), "{}", "Spigen", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Spigen. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (1).jpg\",\"/image/phone case/images (10).jpg\"]", false, "Spigen Premium Accessories Model 1", 5200000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000042"), "{}", "Anker", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Anker. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (11).jpg\",\"/image/phone case/images (12).jpg\"]", false, "Anker Premium Accessories Model 2", 1400000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000043"), "{}", "UAG", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu UAG. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (13).jpg\",\"/image/phone case/images (2).jpg\"]", false, "UAG Premium Accessories Model 3", 8500000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000044"), "{}", "Nillkin", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Nillkin. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (26).jpg\",\"/image/phone case/images (3).jpg\"]", false, "Nillkin Premium Accessories Model 4", 10700000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000045"), "{}", "Baseus", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Baseus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (4).jpg\",\"/image/phone case/images (5).jpg\"]", false, "Baseus Premium Accessories Model 5", 10600000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000046"), "{}", "Spigen", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Spigen. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (6).jpg\",\"/image/phone case/images (7).jpg\"]", false, "Spigen Premium Accessories Model 6", 6000000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000047"), "{}", "Anker", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Anker. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (8).jpg\",\"/image/phone case/images (9).jpg\"]", false, "Anker Premium Accessories Model 7", 20300000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000048"), "{}", "UAG", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu UAG. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images.jpg\",\"/image/phone case/images (1).jpg\"]", false, "UAG Premium Accessories Model 8", 9500000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000049"), "{}", "Nillkin", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Nillkin. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (10).jpg\",\"/image/phone case/images (11).jpg\"]", false, "Nillkin Premium Accessories Model 9", 12700000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000050"), "{}", "Baseus", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đây là sản phẩm Accessories cao cấp từ thương hiệu Baseus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", "[\"/image/phone case/images (12).jpg\",\"/image/phone case/images (13).jpg\"]", false, "Baseus Premium Accessories Model 10", 11900000m, null }
                });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ProductId", "ReservedQuantity", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("e0000000-0000-0000-0000-000000000011"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000011"), 0, 36, null },
                    { new Guid("e0000000-0000-0000-0000-000000000012"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000012"), 0, 50, null },
                    { new Guid("e0000000-0000-0000-0000-000000000013"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000013"), 0, 42, null },
                    { new Guid("e0000000-0000-0000-0000-000000000014"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000014"), 0, 48, null },
                    { new Guid("e0000000-0000-0000-0000-000000000015"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000015"), 0, 19, null },
                    { new Guid("e0000000-0000-0000-0000-000000000016"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000016"), 0, 46, null },
                    { new Guid("e0000000-0000-0000-0000-000000000017"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000017"), 0, 29, null },
                    { new Guid("e0000000-0000-0000-0000-000000000018"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000018"), 0, 18, null },
                    { new Guid("e0000000-0000-0000-0000-000000000019"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000019"), 0, 31, null },
                    { new Guid("e0000000-0000-0000-0000-000000000020"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000020"), 0, 49, null },
                    { new Guid("e0000000-0000-0000-0000-000000000021"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000021"), 0, 49, null },
                    { new Guid("e0000000-0000-0000-0000-000000000022"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000022"), 0, 56, null },
                    { new Guid("e0000000-0000-0000-0000-000000000023"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000023"), 0, 34, null },
                    { new Guid("e0000000-0000-0000-0000-000000000024"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000024"), 0, 51, null },
                    { new Guid("e0000000-0000-0000-0000-000000000025"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000025"), 0, 32, null },
                    { new Guid("e0000000-0000-0000-0000-000000000026"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000026"), 0, 58, null },
                    { new Guid("e0000000-0000-0000-0000-000000000027"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000027"), 0, 56, null },
                    { new Guid("e0000000-0000-0000-0000-000000000028"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000028"), 0, 51, null },
                    { new Guid("e0000000-0000-0000-0000-000000000029"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000029"), 0, 33, null },
                    { new Guid("e0000000-0000-0000-0000-000000000030"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000030"), 0, 12, null },
                    { new Guid("e0000000-0000-0000-0000-000000000031"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000031"), 0, 37, null },
                    { new Guid("e0000000-0000-0000-0000-000000000032"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000032"), 0, 12, null },
                    { new Guid("e0000000-0000-0000-0000-000000000033"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000033"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000034"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000034"), 0, 48, null },
                    { new Guid("e0000000-0000-0000-0000-000000000035"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000035"), 0, 54, null },
                    { new Guid("e0000000-0000-0000-0000-000000000036"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000036"), 0, 48, null },
                    { new Guid("e0000000-0000-0000-0000-000000000037"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000037"), 0, 58, null },
                    { new Guid("e0000000-0000-0000-0000-000000000038"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000038"), 0, 55, null },
                    { new Guid("e0000000-0000-0000-0000-000000000039"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000039"), 0, 39, null },
                    { new Guid("e0000000-0000-0000-0000-000000000040"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000040"), 0, 24, null },
                    { new Guid("e0000000-0000-0000-0000-000000000041"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000041"), 0, 49, null },
                    { new Guid("e0000000-0000-0000-0000-000000000042"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000042"), 0, 40, null },
                    { new Guid("e0000000-0000-0000-0000-000000000043"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000043"), 0, 50, null },
                    { new Guid("e0000000-0000-0000-0000-000000000044"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000044"), 0, 59, null },
                    { new Guid("e0000000-0000-0000-0000-000000000045"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000045"), 0, 21, null },
                    { new Guid("e0000000-0000-0000-0000-000000000046"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000046"), 0, 28, null },
                    { new Guid("e0000000-0000-0000-0000-000000000047"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000047"), 0, 45, null },
                    { new Guid("e0000000-0000-0000-0000-000000000048"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000048"), 0, 29, null },
                    { new Guid("e0000000-0000-0000-0000-000000000049"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000049"), 0, 45, null },
                    { new Guid("e0000000-0000-0000-0000-000000000050"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000050"), 0, 47, null }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "AttributesJson", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "ProductId", "ReservedQuantity", "Sku", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000016"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 4600000m, new Guid("d0000000-0000-0000-0000-000000000011"), 0, "APP-11-0", 15, null },
                    { new Guid("a0000000-0000-0000-0000-000000000017"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 5600000m, new Guid("d0000000-0000-0000-0000-000000000011"), 0, "APP-11-1", 10, null },
                    { new Guid("a0000000-0000-0000-0000-000000000018"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 15800000m, new Guid("d0000000-0000-0000-0000-000000000012"), 0, "SAM-12-0", 6, null },
                    { new Guid("a0000000-0000-0000-0000-000000000019"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 12500000m, new Guid("d0000000-0000-0000-0000-000000000013"), 0, "XIA-13-0", 9, null },
                    { new Guid("a0000000-0000-0000-0000-000000000020"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 13500000m, new Guid("d0000000-0000-0000-0000-000000000013"), 0, "XIA-13-1", 22, null },
                    { new Guid("a0000000-0000-0000-0000-000000000021"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 17200000m, new Guid("d0000000-0000-0000-0000-000000000014"), 0, "OPP-14-0", 7, null },
                    { new Guid("a0000000-0000-0000-0000-000000000022"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 17700000m, new Guid("d0000000-0000-0000-0000-000000000015"), 0, "VIV-15-0", 20, null },
                    { new Guid("a0000000-0000-0000-0000-000000000023"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 18700000m, new Guid("d0000000-0000-0000-0000-000000000015"), 0, "VIV-15-1", 23, null },
                    { new Guid("a0000000-0000-0000-0000-000000000024"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 1300000m, new Guid("d0000000-0000-0000-0000-000000000016"), 0, "APP-16-0", 8, null },
                    { new Guid("a0000000-0000-0000-0000-000000000025"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 9700000m, new Guid("d0000000-0000-0000-0000-000000000017"), 0, "SAM-17-0", 24, null },
                    { new Guid("a0000000-0000-0000-0000-000000000026"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 10700000m, new Guid("d0000000-0000-0000-0000-000000000017"), 0, "SAM-17-1", 20, null },
                    { new Guid("a0000000-0000-0000-0000-000000000027"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 11100000m, new Guid("d0000000-0000-0000-0000-000000000018"), 0, "XIA-18-0", 24, null },
                    { new Guid("a0000000-0000-0000-0000-000000000028"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 12300000m, new Guid("d0000000-0000-0000-0000-000000000019"), 0, "OPP-19-0", 23, null },
                    { new Guid("a0000000-0000-0000-0000-000000000029"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 13300000m, new Guid("d0000000-0000-0000-0000-000000000019"), 0, "OPP-19-1", 19, null },
                    { new Guid("a0000000-0000-0000-0000-000000000030"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 6800000m, new Guid("d0000000-0000-0000-0000-000000000020"), 0, "VIV-20-0", 5, null },
                    { new Guid("a0000000-0000-0000-0000-000000000031"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 13200000m, new Guid("d0000000-0000-0000-0000-000000000021"), 0, "LOG-21-0", 7, null },
                    { new Guid("a0000000-0000-0000-0000-000000000032"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 14200000m, new Guid("d0000000-0000-0000-0000-000000000021"), 0, "LOG-21-1", 12, null },
                    { new Guid("a0000000-0000-0000-0000-000000000033"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 12100000m, new Guid("d0000000-0000-0000-0000-000000000022"), 0, "RAZ-22-0", 10, null },
                    { new Guid("a0000000-0000-0000-0000-000000000034"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 14600000m, new Guid("d0000000-0000-0000-0000-000000000023"), 0, "COR-23-0", 11, null },
                    { new Guid("a0000000-0000-0000-0000-000000000035"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 15600000m, new Guid("d0000000-0000-0000-0000-000000000023"), 0, "COR-23-1", 24, null },
                    { new Guid("a0000000-0000-0000-0000-000000000036"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 17000000m, new Guid("d0000000-0000-0000-0000-000000000024"), 0, "STE-24-0", 20, null },
                    { new Guid("a0000000-0000-0000-0000-000000000037"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 1400000m, new Guid("d0000000-0000-0000-0000-000000000025"), 0, "HYP-25-0", 11, null },
                    { new Guid("a0000000-0000-0000-0000-000000000038"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 2400000m, new Guid("d0000000-0000-0000-0000-000000000025"), 0, "HYP-25-1", 12, null },
                    { new Guid("a0000000-0000-0000-0000-000000000039"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 17000000m, new Guid("d0000000-0000-0000-0000-000000000026"), 0, "LOG-26-0", 12, null },
                    { new Guid("a0000000-0000-0000-0000-000000000040"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 8300000m, new Guid("d0000000-0000-0000-0000-000000000027"), 0, "RAZ-27-0", 22, null },
                    { new Guid("a0000000-0000-0000-0000-000000000041"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 9300000m, new Guid("d0000000-0000-0000-0000-000000000027"), 0, "RAZ-27-1", 17, null },
                    { new Guid("a0000000-0000-0000-0000-000000000042"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 17400000m, new Guid("d0000000-0000-0000-0000-000000000028"), 0, "COR-28-0", 15, null },
                    { new Guid("a0000000-0000-0000-0000-000000000043"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 16400000m, new Guid("d0000000-0000-0000-0000-000000000029"), 0, "STE-29-0", 23, null },
                    { new Guid("a0000000-0000-0000-0000-000000000044"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 17400000m, new Guid("d0000000-0000-0000-0000-000000000029"), 0, "STE-29-1", 18, null },
                    { new Guid("a0000000-0000-0000-0000-000000000045"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 15400000m, new Guid("d0000000-0000-0000-0000-000000000030"), 0, "HYP-30-0", 21, null },
                    { new Guid("a0000000-0000-0000-0000-000000000046"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 9400000m, new Guid("d0000000-0000-0000-0000-000000000031"), 0, "SON-31-0", 6, null },
                    { new Guid("a0000000-0000-0000-0000-000000000047"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 10400000m, new Guid("d0000000-0000-0000-0000-000000000031"), 0, "SON-31-1", 22, null },
                    { new Guid("a0000000-0000-0000-0000-000000000048"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 9500000m, new Guid("d0000000-0000-0000-0000-000000000032"), 0, "JBL-32-0", 11, null },
                    { new Guid("a0000000-0000-0000-0000-000000000049"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 14100000m, new Guid("d0000000-0000-0000-0000-000000000033"), 0, "BOS-33-0", 14, null },
                    { new Guid("a0000000-0000-0000-0000-000000000050"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 15100000m, new Guid("d0000000-0000-0000-0000-000000000033"), 0, "BOS-33-1", 20, null },
                    { new Guid("a0000000-0000-0000-0000-000000000051"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 16700000m, new Guid("d0000000-0000-0000-0000-000000000034"), 0, "MAR-34-0", 14, null },
                    { new Guid("a0000000-0000-0000-0000-000000000052"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 1600000m, new Guid("d0000000-0000-0000-0000-000000000035"), 0, "SEN-35-0", 14, null },
                    { new Guid("a0000000-0000-0000-0000-000000000053"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 2600000m, new Guid("d0000000-0000-0000-0000-000000000035"), 0, "SEN-35-1", 5, null },
                    { new Guid("a0000000-0000-0000-0000-000000000054"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 10300000m, new Guid("d0000000-0000-0000-0000-000000000036"), 0, "SON-36-0", 24, null },
                    { new Guid("a0000000-0000-0000-0000-000000000055"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 10400000m, new Guid("d0000000-0000-0000-0000-000000000037"), 0, "JBL-37-0", 15, null },
                    { new Guid("a0000000-0000-0000-0000-000000000056"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 11400000m, new Guid("d0000000-0000-0000-0000-000000000037"), 0, "JBL-37-1", 8, null },
                    { new Guid("a0000000-0000-0000-0000-000000000057"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 17800000m, new Guid("d0000000-0000-0000-0000-000000000038"), 0, "BOS-38-0", 16, null },
                    { new Guid("a0000000-0000-0000-0000-000000000058"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 2700000m, new Guid("d0000000-0000-0000-0000-000000000039"), 0, "MAR-39-0", 5, null },
                    { new Guid("a0000000-0000-0000-0000-000000000059"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 3700000m, new Guid("d0000000-0000-0000-0000-000000000039"), 0, "MAR-39-1", 24, null },
                    { new Guid("a0000000-0000-0000-0000-000000000060"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 7600000m, new Guid("d0000000-0000-0000-0000-000000000040"), 0, "SEN-40-0", 17, null },
                    { new Guid("a0000000-0000-0000-0000-000000000061"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 5200000m, new Guid("d0000000-0000-0000-0000-000000000041"), 0, "SPI-41-0", 20, null },
                    { new Guid("a0000000-0000-0000-0000-000000000062"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 6200000m, new Guid("d0000000-0000-0000-0000-000000000041"), 0, "SPI-41-1", 12, null },
                    { new Guid("a0000000-0000-0000-0000-000000000063"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 1400000m, new Guid("d0000000-0000-0000-0000-000000000042"), 0, "ANK-42-0", 10, null },
                    { new Guid("a0000000-0000-0000-0000-000000000064"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 8500000m, new Guid("d0000000-0000-0000-0000-000000000043"), 0, "UAG-43-0", 6, null },
                    { new Guid("a0000000-0000-0000-0000-000000000065"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 9500000m, new Guid("d0000000-0000-0000-0000-000000000043"), 0, "UAG-43-1", 15, null },
                    { new Guid("a0000000-0000-0000-0000-000000000066"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 10700000m, new Guid("d0000000-0000-0000-0000-000000000044"), 0, "NIL-44-0", 21, null },
                    { new Guid("a0000000-0000-0000-0000-000000000067"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 10600000m, new Guid("d0000000-0000-0000-0000-000000000045"), 0, "BAS-45-0", 21, null },
                    { new Guid("a0000000-0000-0000-0000-000000000068"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 11600000m, new Guid("d0000000-0000-0000-0000-000000000045"), 0, "BAS-45-1", 21, null },
                    { new Guid("a0000000-0000-0000-0000-000000000069"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 6000000m, new Guid("d0000000-0000-0000-0000-000000000046"), 0, "SPI-46-0", 17, null },
                    { new Guid("a0000000-0000-0000-0000-000000000070"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 20300000m, new Guid("d0000000-0000-0000-0000-000000000047"), 0, "ANK-47-0", 16, null },
                    { new Guid("a0000000-0000-0000-0000-000000000071"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 21300000m, new Guid("d0000000-0000-0000-0000-000000000047"), 0, "ANK-47-1", 7, null },
                    { new Guid("a0000000-0000-0000-0000-000000000072"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 9500000m, new Guid("d0000000-0000-0000-0000-000000000048"), 0, "UAG-48-0", 15, null },
                    { new Guid("a0000000-0000-0000-0000-000000000073"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 12700000m, new Guid("d0000000-0000-0000-0000-000000000049"), 0, "NIL-49-0", 8, null },
                    { new Guid("a0000000-0000-0000-0000-000000000074"), "{\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Cao cấp", 13700000m, new Guid("d0000000-0000-0000-0000-000000000049"), 0, "NIL-49-1", 11, null },
                    { new Guid("a0000000-0000-0000-0000-000000000075"), "{\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Phiên bản Tiêu chuẩn", 11900000m, new Guid("d0000000-0000-0000-0000-000000000050"), 0, "BAS-50-0", 15, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000074"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000075"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000050"));

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000001"),
                column: "StockQuantity",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000002"),
                column: "StockQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000003"),
                column: "StockQuantity",
                value: 50);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000004"),
                column: "StockQuantity",
                value: 45);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000005"),
                column: "StockQuantity",
                value: 100);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000006"),
                column: "StockQuantity",
                value: 30);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000007"),
                column: "StockQuantity",
                value: 40);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000008"),
                column: "StockQuantity",
                value: 80);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000009"),
                column: "StockQuantity",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000010"),
                column: "StockQuantity",
                value: 150);

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "DiscountAmount", "IsDeleted", "Note", "PaymentMethod", "PaymentStatus", "RecipientName", "RecipientPhone", "ShippingAddress", "Status", "TotalAmount", "UpdatedAt", "UserId", "VoucherId" },
                values: new object[] { new Guid("f0000000-0000-0000-0000-000000000001"), new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), 0m, false, null, "MoMo", "Unpaid", "Nguyễn Văn An", "0901234567", "Số 10 Nguyễn Huệ, Quận 1, TP.HCM", "Delivered", 29990000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"), null });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000001"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"ram\":\"36GB Unified\",\"storage\":\"1TB SSD\",\"color\":\"Silver\",\"cpu\":\"M3 Max 14-core\",\"gpu\":\"30-core GPU\"}", "36GB RAM / 1TB SSD / Silver", 89990000m, "MAC-M3M-16-1TB-SLV", 10 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000002"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"ram\":\"36GB Unified\",\"storage\":\"1TB SSD\",\"color\":\"Space Black\",\"cpu\":\"M3 Max 14-core\",\"gpu\":\"30-core GPU\"}", "36GB RAM / 1TB SSD / Space Black", 89990000m, "MAC-M3M-16-1TB-BLK", 5 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000003"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"ram\":\"64GB DDR5\",\"storage\":\"2TB SSD Gen4\",\"color\":\"Off Black\",\"cpu\":\"Intel Core i9-14900HX\",\"gpu\":\"NVIDIA RTX 4090 16GB\"}", "64GB RAM / 2TB SSD / RTX 4090", 95000000m, "ASUS-SCAR18-4090", 3 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000004"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"storage\":\"256GB\",\"color\":\"Natural Titanium\",\"screen\":\"6.7 inch Super Retina XDR\"}", "256GB / Natural Titanium", 29990000m, "IP15PM-256-NAT", 20 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000005"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"storage\":\"512GB\",\"color\":\"White Titanium\",\"screen\":\"6.7 inch Super Retina XDR\"}", "512GB / White Titanium", 34990000m, "IP15PM-512-WHT", 15 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000006"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"ram\":\"12GB\",\"storage\":\"512GB\",\"color\":\"Titanium Black\",\"cpu\":\"Snapdragon 8 Gen 3 for Galaxy\"}", "12GB RAM / 512GB / Titanium Black", 32490000m, "S24U-512-BLK", 12 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000007"),
                columns: new[] { "AttributesJson", "Name", "Price", "Sku", "StockQuantity" },
                values: new object[] { "{\"switch\":\"GL Tactile\",\"color\":\"Black\",\"connection\":\"Lightspeed Wireless\"}", "GL Tactile / Black", 3790000m, "LOGI-G915-TKL", 30 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000008"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"switch\":\"Razer Green\",\"color\":\"Black\",\"connection\":\"Wired\"}", "Green Switch / Black", 5990000m, new Guid("d0000000-0000-0000-0000-000000000006"), "RAZER-BW-V4", 25 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000009"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\",\"connection\":\"Bluetooth 5.2\"}", "Standard Edition", 14490000m, new Guid("d0000000-0000-0000-0000-000000000007"), "SONY-XV800", 10 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000010"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\",\"battery\":\"20 Hours\"}", "Black", 3490000m, new Guid("d0000000-0000-0000-0000-000000000008"), "JBL-CHG5-BLK", 50 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("a0000000-0000-0000-0000-000000000011"),
                columns: new[] { "AttributesJson", "Name", "Price", "ProductId", "Sku", "StockQuantity" },
                values: new object[] { "{\"color\":\"Black\",\"power\":\"65W\",\"ports\":\"2C 1A\"}", "65W / Black", 1290000m, new Guid("d0000000-0000-0000-0000-000000000009"), "ANKER-735-BLK", 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                columns: new[] { "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Siêu phẩm laptop đồ họa cao cấp nhất từ Apple với chip M3 Max 16-core CPU, 40-core GPU. Màn hình Liquid Retina XDR 120Hz siêu sắc nét. Phù hợp cho dân thiết kế 3D, dựng phim chuyên nghiệp.", "[\"/image/latop/images (1).jpg\",\"/image/latop/images (2).jpg\",\"/image/latop/images (3).jpg\"]", "MacBook Pro 16 M3 Max", 89990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                columns: new[] { "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Quái thú gaming đích thực với cấu hình khủng Intel Core i9-14900HX, RTX 4090 16GB. Màn hình 18 inch ROG Nebula HDR 240Hz. Hệ thống tản nhiệt thông minh Tri-Fan.", "[\"/image/latop/images (4).jpg\",\"/image/latop/images (5).jpg\",\"/image/latop/images (6).jpg\"]", "Asus ROG Strix SCAR 18", 95000000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Apple", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), "Điện thoại flagship cao cấp với khung Titanium siêu nhẹ, siêu bền. Chip A17 Pro mạnh mẽ cho trải nghiệm gaming mượt mà, hệ thống camera telephoto 5x độc quyền.", "[\"/image/phone/images (1).jpg\",\"/image/phone/images (2).jpg\",\"/image/phone/images (3).jpg\"]", "iPhone 15 Pro Max", 29990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Samsung", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), "Điện thoại AI đầu tiên thế giới với tính năng Galaxy AI đột phá. Bút S-Pen tích hợp, khung viền Titanium, camera 200MP siêu zoom 100x cực nét.", "[\"/image/phone/images (4).jpg\",\"/image/phone/images (5).jpg\",\"/image/phone/images (6).jpg\"]", "Samsung Galaxy S24 Ultra", 32490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Logitech", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), "Bàn phím cơ không dây siêu mỏng, trang bị switch GL Low Profile cho tốc độ phản hồi cực nhanh. Hệ thống LED RGB LIGHTSYNC rực rỡ và thời lượng pin cực khủng.", "[\"/image/keyboard/images (1).jpg\",\"/image/keyboard/images (2).jpg\"]", "Logitech G915 TKL Wireless", 3790000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Razer", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), "Bàn phím cơ full-size với hệ thống LED RGB Chroma rực rỡ dưới viền, trang bị Macro keys và núm xoay Command Dial đa năng.", "[\"/image/keyboard/images (3).jpg\",\"/image/keyboard/images (4).jpg\"]", "Razer BlackWidow V4 Pro", 5990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Sony", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), "Loa Bluetooth không dây công suất lớn với âm thanh 360 độ cực đỉnh. Tích hợp đèn LED đa sắc và cổng cắm micro hát karaoke chuyên nghiệp.", "[\"/image/speaker/images (1).jpg\",\"/image/speaker/images (2).jpg\"]", "Sony SRS-XV800 Wireless", 14490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "JBL", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), "Loa di động chống nước chuẩn IP67 với âm thanh Original Pro Sound cực mạnh. Thời lượng pin 20 tiếng, tích hợp sạc dự phòng vô cùng tiện lợi.", "[\"/image/speaker/images (3).jpg\",\"/image/speaker/images (4).jpg\"]", "JBL Charge 5", 3490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Anker", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), "Củ sạc siêu nhanh 65W với công nghệ GaN thế hệ mới. Trang bị 2 cổng USB-C và 1 cổng USB-A, thiết kế siêu nhỏ gọn và an toàn tuyệt đối.", "[\"/image/charger/images (2).jpg\",\"/image/charger/images (3).jpg\"]", "Anker 735 Charger (GaNPrime 65W)", 1290000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000010"),
                columns: new[] { "Brand", "CategoryId", "Description", "ImageUrls", "Name", "Price" },
                values: new object[] { "Spigen", new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), "Ốp lưng siêu chống sốc với công nghệ Air Cushion độc quyền. Thiết kế 2 lớp bảo vệ cực tốt kèm chân chống tiện lợi cho việc xem video.", "[\"/image/phone case/images (1).jpg\",\"/image/phone case/images (2).jpg\"]", "Spigen Tough Armor Case iPhone 15", 850000m });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "OrderId", "ProductId", "ProductVariantId", "Quantity", "UnitPrice", "UpdatedAt", "VariantSnapshotJson" },
                values: new object[] { new Guid("f1000000-0000-0000-0000-000000000001"), new DateTime(2026, 7, 4, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000001"), new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("a0000000-0000-0000-0000-000000000004"), 1, 29990000m, null, "{}" });

            migrationBuilder.InsertData(
                table: "ProductReviews",
                columns: new[] { "Id", "AdminReply", "Comment", "CreatedAt", "ImageUrls", "IsDeleted", "OrderId", "ProductId", "Rating", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("70000000-0000-0000-0000-000000000001"), null, "Máy siêu mạnh, render video 4K nhanh như chớp. Màn hình đẹp xuất sắc, pin trâu dùng cả ngày không hết.", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("f0000000-0000-0000-0000-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), 5, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("70000000-0000-0000-0000-000000000002"), null, "Màu Space Black cực kỳ sang trọng, ít bám vân tay hơn. Bàn phím gõ êm nhưng giá hơi chát.", new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("f0000000-0000-0000-0000-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), 4, null, new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c") },
                    { new Guid("70000000-0000-0000-0000-000000000003"), null, "Cầm rất nhẹ so với đời 14 Pro Max. Camera zoom 5x chụp ảnh chân dung cực kỳ sắc nét. Quá tuyệt vời!", new DateTime(2026, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("f0000000-0000-0000-0000-000000000001"), new Guid("d0000000-0000-0000-0000-000000000003"), 5, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("70000000-0000-0000-0000-000000000004"), null, "Tính năng phiên dịch trực tiếp AI cực kỳ hữu ích khi đi du lịch. Màn hình chống chói rất ngon.", new DateTime(2026, 6, 27, 0, 0, 0, 0, DateTimeKind.Utc), null, false, new Guid("f0000000-0000-0000-0000-000000000001"), new Guid("d0000000-0000-0000-0000-000000000004"), 5, null, new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c") }
                });
        }
    }
}
