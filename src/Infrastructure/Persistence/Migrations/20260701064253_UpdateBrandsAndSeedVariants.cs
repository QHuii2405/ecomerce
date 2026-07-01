using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBrandsAndSeedVariants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AttributesJson",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductVariantId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VariantSnapshotJson",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QrCodeUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RawResponse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductReviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sku = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributesJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    ReservedQuantity = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000001"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000002"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000003"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000004"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000005"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000006"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000007"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000008"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000009"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000010"),
                columns: new[] { "ProductVariantId", "VariantSnapshotJson" },
                values: new object[] { null, "{}" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000001"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000002"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000003"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000004"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000005"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000006"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000007"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000008"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000009"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000010"),
                column: "PaymentStatus",
                value: "Unpaid");

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "AttributesJson", "CreatedAt", "IsActive", "IsDeleted", "Name", "Price", "ProductId", "ReservedQuantity", "Sku", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a0000000-0000-0000-0000-000000000001"), "{\"ram\":\"16GB\",\"storage\":\"512GB\",\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "16GB / 512GB / Black", 52490000m, new Guid("d0000000-0000-0000-0000-000000000001"), 0, "DELL-XPS15-16-512-BLK", 10, null },
                    { new Guid("a0000000-0000-0000-0000-000000000002"), "{\"ram\":\"32GB\",\"storage\":\"1TB\",\"color\":\"Silver\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "32GB / 1TB / Silver", 59990000m, new Guid("d0000000-0000-0000-0000-000000000001"), 0, "DELL-XPS15-32-1TB-SLV", 5, null },
                    { new Guid("a0000000-0000-0000-0000-000000000003"), "{\"ram\":\"16GB\",\"storage\":\"512GB\",\"color\":\"Silver\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "16GB / 512GB / Silver", 28990000m, new Guid("d0000000-0000-0000-0000-000000000002"), 0, "MAC-AIR15-16-512-SLV", 15, null },
                    { new Guid("a0000000-0000-0000-0000-000000000004"), "{\"ram\":\"32GB\",\"storage\":\"1TB\",\"color\":\"Midnight\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "32GB / 1TB / Midnight", 34990000m, new Guid("d0000000-0000-0000-0000-000000000002"), 0, "MAC-AIR15-32-1TB-BLK", 8, null },
                    { new Guid("a0000000-0000-0000-0000-000000000005"), "{\"storage\":\"256GB\",\"color\":\"Natural Titanium\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "256GB / Natural Titanium", 29990000m, new Guid("d0000000-0000-0000-0000-000000000031"), 0, "IP15PM-256-BLK", 20, null },
                    { new Guid("a0000000-0000-0000-0000-000000000006"), "{\"storage\":\"512GB\",\"color\":\"Blue Titanium\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "512GB / Blue Titanium", 34990000m, new Guid("d0000000-0000-0000-0000-000000000031"), 0, "IP15PM-512-BLU", 10, null },
                    { new Guid("a0000000-0000-0000-0000-000000000007"), "{\"storage\":\"256GB\",\"color\":\"Titanium Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "256GB / Titanium Black", 28990000m, new Guid("d0000000-0000-0000-0000-000000000032"), 0, "S24U-256-BLK", 25, null },
                    { new Guid("a0000000-0000-0000-0000-000000000008"), "{\"storage\":\"512GB\",\"color\":\"Titanium Yellow\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "512GB / Titanium Yellow", 32990000m, new Guid("d0000000-0000-0000-0000-000000000032"), 0, "S24U-512-YLW", 12, null },
                    { new Guid("a0000000-0000-0000-0000-000000000009"), "{\"switch\":\"Red\",\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Red Switch / Black", 4725000m, new Guid("d0000000-0000-0000-0000-000000000011"), 0, "LOGI-GPX-RED-BLK", 30, null },
                    { new Guid("a0000000-0000-0000-0000-000000000010"), "{\"switch\":\"Blue\",\"color\":\"White\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Blue Switch / White", 4725000m, new Guid("d0000000-0000-0000-0000-000000000011"), 0, "LOGI-GPX-BLU-WHT", 15, null },
                    { new Guid("a0000000-0000-0000-0000-000000000011"), "{\"switch\":\"Brown\",\"color\":\"Black\"}", new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), true, false, "Brown Switch / Black", 890000m, new Guid("d0000000-0000-0000-0000-000000000013"), 0, "ASUS-FAL-BRN-BLK", 20, null }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Dell", "Dell XPS 15" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                columns: new[] { "AttributesJson", "Brand", "Description", "Name" },
                values: new object[] { "{}", "Apple", "Laptop văn phòng 15 inch nhẹ 1.2kg, pin 20 giờ, M3 chip.", "MacBook Air 15" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Asus", "Asus ROG Zephyrus G14" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Lenovo", "Lenovo ThinkPad X1" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "HP", "HP Spectre x360" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "MSI", "MSI Stealth 16" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Dell", "Dell Alienware m16" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Apple", "MacBook Pro 14" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Asus", "Asus Zenbook 14" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000010"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Lenovo", "Lenovo Legion Pro 5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000011"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Logitech", "Logitech G Pro X" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000012"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Razer", "Razer DeathAdder V3 Pro" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000013"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Asus", "Asus ROG Falchion" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000014"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "MSI", "MSI Vigor GK71" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000015"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Logitech", "Logitech G915 TKL" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000016"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Razer", "Razer Huntsman V2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000017"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Asus", "Asus ROG Gladius III" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000018"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "MSI", "MSI Clutch GM41" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000019"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Logitech", "Logitech G733" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000020"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Razer", "Razer BlackShark V2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000021"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Sony", "Sony WH-1000XM5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000022"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Apple", "AirPods Pro 2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000023"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Samsung", "Samsung Galaxy Buds 2" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000024"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Sony", "Sony WF-1000XM5" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000025"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Apple", "AirPods Max" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000026"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "iLuminaty", "iLuminaty SoundCore" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000027"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Sony", "Sony SRS-XG300" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000028"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Samsung", "Samsung Soundbar Q990C" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000029"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "iLuminaty", "iLuminaty Boom 360" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000030"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Apple", "Apple HomePod" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000031"),
                columns: new[] { "AttributesJson", "Brand", "Name", "Price" },
                values: new object[] { "{}", "Apple", "iPhone 15 Pro Max", 29990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000032"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Samsung", "Samsung Galaxy S24 Ultra" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000033"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Xiaomi", "Xiaomi 14 Pro" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000034"),
                columns: new[] { "AttributesJson", "Brand", "Name", "Price" },
                values: new object[] { "{}", "Oppo", "Oppo Find X7 Ultra", 25990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000035"),
                columns: new[] { "AttributesJson", "Brand", "Name", "Price" },
                values: new object[] { "{}", "Apple", "iPhone 14", 19490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000036"),
                columns: new[] { "AttributesJson", "Brand", "Name", "Price" },
                values: new object[] { "{}", "Samsung", "Samsung Galaxy Z Fold 5", 32490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000037"),
                columns: new[] { "AttributesJson", "Brand", "Name" },
                values: new object[] { "{}", "Xiaomi", "Xiaomi Redmi Note 13" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000038"),
                columns: new[] { "AttributesJson", "Brand", "Name", "Price" },
                values: new object[] { "{}", "Oppo", "Oppo Reno 11", 11990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000039"),
                columns: new[] { "AttributesJson", "Brand", "Name", "Price" },
                values: new object[] { "{}", "Samsung", "Samsung Galaxy A55", 9990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000040"),
                columns: new[] { "AttributesJson", "Brand", "Name", "Price" },
                values: new object[] { "{}", "Apple", "iPhone 13", 13490000m });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductVariantId",
                table: "OrderItems",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_OrderId",
                table: "PaymentTransactions",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_OrderId",
                table: "ProductReviews",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_ProductId_UserId",
                table: "ProductReviews",
                columns: new[] { "ProductId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductReviews_UserId",
                table: "ProductReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductVariantId",
                table: "OrderItems",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVariants_ProductVariantId",
                table: "OrderItems");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

            migrationBuilder.DropTable(
                name: "ProductReviews");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductVariantId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "AttributesJson",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductVariantId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "VariantSnapshotJson",
                table: "OrderItems");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"),
                column: "Name",
                value: "iLuminaty Pro X1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Laptop văn phòng 15 inch nhẹ 1.2kg, pin 20 giờ, AMD Ryzen 7 8845HS.", "iLuminaty Air 15" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"),
                column: "Name",
                value: "Phantom Studio Pro");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"),
                column: "Name",
                value: "Nexus Book S13");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"),
                column: "Name",
                value: "Lumina Elite X Pro");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"),
                column: "Name",
                value: "ProVision 16 Max");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"),
                column: "Name",
                value: "Swift Creator X");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"),
                column: "Name",
                value: "Nova Slim 14");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"),
                column: "Name",
                value: "Titan Gamer Pro");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000010"),
                column: "Name",
                value: "CodeBook Developer Ed.");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000011"),
                column: "Name",
                value: "Ghost Mechanical Keyboard");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000012"),
                column: "Name",
                value: "Phantom Mouse Pro X");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000013"),
                column: "Name",
                value: "HyperPad XL RGB");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000014"),
                column: "Name",
                value: "Storm Gaming Chair");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000015"),
                column: "Name",
                value: "NitroView 27\" 165Hz");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000016"),
                column: "Name",
                value: "StreamDeck Pro");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000017"),
                column: "Name",
                value: "VortexKeys 60%");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000018"),
                column: "Name",
                value: "CoreController Pro");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000019"),
                column: "Name",
                value: "UltraCapture 4K Cam");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000020"),
                column: "Name",
                value: "Aurora Gaming Headset");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000021"),
                column: "Name",
                value: "Sonic Buds Pro");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000022"),
                column: "Name",
                value: "CrystalAir Studio Mic");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000023"),
                column: "Name",
                value: "BassBoom Speaker 360");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000024"),
                column: "Name",
                value: "Lumina Over-Ear H1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000025"),
                column: "Name",
                value: "AudioBar Pro 2.1");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000026"),
                column: "Name",
                value: "VoiceLink Pro Earbuds");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000027"),
                column: "Name",
                value: "PocketDAC Ultra");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000028"),
                column: "Name",
                value: "SonicWave IEM S5");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000029"),
                column: "Name",
                value: "ClearCast Podcast Kit");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000030"),
                column: "Name",
                value: "HomeAudio Amp X3");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000031"),
                columns: new[] { "Name", "Price" },
                values: new object[] { "Nexus Watch S", 9975000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000032"),
                column: "Name",
                value: "iLuminaty Phone Pro");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000033"),
                column: "Name",
                value: "NovaPad Ultra 12");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000034"),
                columns: new[] { "Name", "Price" },
                values: new object[] { "FoldX Premium", 45990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000035"),
                columns: new[] { "Name", "Price" },
                values: new object[] { "MidRange Hero A55", 9490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000036"),
                columns: new[] { "Name", "Price" },
                values: new object[] { "WristBand Fit Pro", 2490000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000037"),
                column: "Name",
                value: "AirTab Kids 8");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000038"),
                columns: new[] { "Name", "Price" },
                values: new object[] { "PowerBank Turbo 25K", 1990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000039"),
                columns: new[] { "Name", "Price" },
                values: new object[] { "SmartGlasses AR-1", 15990000m });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000040"),
                columns: new[] { "Name", "Price" },
                values: new object[] { "ChargeStation 8-in-1", 3490000m });
        }
    }
}
