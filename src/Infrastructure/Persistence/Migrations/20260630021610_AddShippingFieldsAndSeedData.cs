using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingFieldsAndSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SavedAddresses",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecipientPhone",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"),
                column: "Description",
                value: "Laptops & Máy tính xách tay cao cấp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"),
                column: "Description",
                value: "Gaming Gear & Phụ kiện Gaming");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"),
                column: "Description",
                value: "Thiết bị Âm thanh Cao cấp");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"),
                column: "Description",
                value: "Smartphone & Thiết bị Di động");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "Note", "PaymentMethod", "RecipientName", "RecipientPhone", "ShippingAddress", "Status", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("f0000000-0000-0000-0000-000000000001"), new DateTime(2026, 6, 15, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "COD", "Trần Thị Bình", "0900000001", "Số 10 Nguyễn Huệ, Quận 1, TP.HCM", "Pending", 52490000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 16, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "VietQR", "Nguyễn Văn An", "0900000002", "Số 20 Nguyễn Huệ, Quận 1, TP.HCM", "Pending", 52490000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000003"), new DateTime(2026, 6, 17, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "MoMo", "Trần Thị Bình", "0900000003", "Số 30 Nguyễn Huệ, Quận 1, TP.HCM", "Pending", 9450000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "COD", "Nguyễn Văn An", "0900000004", "Số 40 Nguyễn Huệ, Quận 1, TP.HCM", "Confirmed", 4725000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000005"), new DateTime(2026, 6, 19, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "VietQR", "Trần Thị Bình", "0900000005", "Số 50 Nguyễn Huệ, Quận 1, TP.HCM", "Confirmed", 6225000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 20, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "MoMo", "Nguyễn Văn An", "0900000006", "Số 60 Nguyễn Huệ, Quận 1, TP.HCM", "Shipping", 12450000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000007"), new DateTime(2026, 6, 21, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "COD", "Trần Thị Bình", "0900000007", "Số 70 Nguyễn Huệ, Quận 1, TP.HCM", "Shipping", 28990000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000008"), new DateTime(2026, 6, 22, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "VietQR", "Nguyễn Văn An", "0900000008", "Số 80 Nguyễn Huệ, Quận 1, TP.HCM", "Delivered", 28990000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000009"), new DateTime(2026, 6, 23, 8, 0, 0, 0, DateTimeKind.Utc), false, null, "MoMo", "Trần Thị Bình", "0900000009", "Số 90 Nguyễn Huệ, Quận 1, TP.HCM", "Delivered", 18980000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") },
                    { new Guid("f0000000-0000-0000-0000-000000000010"), new DateTime(2026, 6, 24, 8, 0, 0, 0, DateTimeKind.Utc), false, "Hủy do thay đổi kế hoạch", "COD", "Nguyễn Văn An", "0900000010", "Số 100 Nguyễn Huệ, Quận 1, TP.HCM", "Cancelled", 9490000m, null, new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "IsDeleted", "Name", "Price", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d0000000-0000-0000-0000-000000000001"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Flagship laptop với Intel Core Ultra 9, RAM 32GB, màn OLED 4K, siêu mỏng 14mm.", false, "iLuminaty Pro X1", 52490000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000002"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Laptop văn phòng 15 inch nhẹ 1.2kg, pin 20 giờ, AMD Ryzen 7 8845HS.", false, "iLuminaty Air 15", 28990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000003"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Workstation sáng tạo với RTX 4090, RAM 64GB ECC, thiết kế CNC aluminium.", false, "Phantom Studio Pro", 89900000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000004"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Ultrabook 13 inch gọn nhẹ, màn hình 2.8K AMOLED 120Hz, Intel Arc GPU.", false, "Nexus Book S13", 22590000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000005"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Business laptop bảo mật cấp cao, TPM 2.0, khuôn mặt IR, RAM 16GB LPDDR5X.", false, "Lumina Elite X Pro", 35990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000006"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Laptop đồ họa chuyên dụng 16 inch, RTX 4070, bàn phím cơ Cherry MX.", false, "ProVision 16 Max", 45990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000007"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Creator laptop màn 4K OLED touch, Ryzen 9 8945HX, card đồ họa Radeon 890M.", false, "Swift Creator X", 38500000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000008"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Mỏng nhẹ nhất phân khúc, chỉ 0.9kg, màn 14 inch QHD+, pin 18 giờ.", false, "Nova Slim 14", 19990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000009"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Gaming laptop 17 inch, RTX 4080 12GB, màn 240Hz QHD, tản nhiệt vapor chamber.", false, "Titan Gamer Pro", 58990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000010"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Laptop cho lập trình viên, Linux ready, 32GB RAM, SSD 2TB NVMe PCIe 5.0.", false, "CodeBook Developer Ed.", 31990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000011"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Bàn phím cơ không dây đỉnh cao, switch êm ái, đèn nền RGB per-key.", false, "Ghost Mechanical Keyboard", 4725000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000012"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Chuột gaming 26000 DPI, sensor quang học, trọng lượng 58g siêu nhẹ.", false, "Phantom Mouse Pro X", 2990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000013"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Mousepad gaming khổng lồ 900×400mm, bề mặt tốc độ cao, viền LED RGB.", false, "HyperPad XL RGB", 890000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000014"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Ghế gaming ergonomic, đệm memory foam, tựa đầu 4D, chịu tải 150kg.", false, "Storm Gaming Chair", 8990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000015"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Màn hình gaming 27 inch QHD 165Hz, 1ms GTG, FreeSync Premium, HDR400.", false, "NitroView 27\" 165Hz", 9990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000016"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Bảng điều khiển stream 32 phím LCD, tích hợp OBS, Twitch, Discord.", false, "StreamDeck Pro", 3490000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000017"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Bàn phím compact 60%, hot-swap socket, keycap PBT double-shot.", false, "VortexKeys 60%", 2190000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000018"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Tay cầm chơi game PC/Mobile, kết nối USB-C + Bluetooth, trigger haptic.", false, "CoreController Pro", 1990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000019"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Webcam gaming 4K 30fps, AI background removal, ring light LED tích hợp.", false, "UltraCapture 4K Cam", 3290000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000020"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Headset gaming 7.1 surround, mic khử ồn AI, đệm tai foam ngủ ngon.", false, "Aurora Gaming Headset", 2490000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000021"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Tai nghe chống ồn ANC thế hệ 3, driver 11mm, pin 36 giờ kèm case.", false, "Sonic Buds Pro", 6225000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000022"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Micro condenser USB-C cho studio, cardioid/omnidirectional, 192kHz.", false, "CrystalAir Studio Mic", 4590000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000023"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Loa Bluetooth 360 độ IPX7, bass mạnh, pin 24 giờ, kết nối đa điểm.", false, "BassBoom Speaker 360", 3190000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000024"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Tai nghe over-ear cao cấp, driver planar 50mm, DAC tích hợp 32-bit.", false, "Lumina Over-Ear H1", 12990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000025"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Thanh loa soundbar 2.1 kèm subwoofer, Dolby Atmos, HDMI ARC.", false, "AudioBar Pro 2.1", 8490000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000026"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "TWS earbuds cho công việc, mic MEMS 4 array, ENC khử ồn, pin 28 giờ.", false, "VoiceLink Pro Earbuds", 3990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000027"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "DAC/Amp di động, ES9038Q2M chip, power 300mW vào 32Ω, USB-C.", false, "PocketDAC Ultra", 5290000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000028"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "In-ear monitor 5 driver hybrid, cáp bạc 8 lõi, dải tần 8Hz-45kHz.", false, "SonicWave IEM S5", 9990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000029"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Bộ podcast: mic arm + mic + pop filter + audio interface USB.", false, "ClearCast Podcast Kit", 4290000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000030"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Amplifier hi-fi cho loa passive, 2×100W class AB, Bluetooth 5.3.", false, "HomeAudio Amp X3", 6990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000031"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Đồng hồ thông minh AMOLED 1.8\", đo SpO2/ECG/BP, GPS độc lập.", false, "Nexus Watch S", 9975000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000032"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Flagship phone Snapdragon 8 Elite, camera 200MP, sạc 100W, IP68.", false, "iLuminaty Phone Pro", 28990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000033"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Tablet 12 inch 2.8K 144Hz, M2 chip, 5G, bút stylus thế hệ 2.", false, "NovaPad Ultra 12", 22490000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000034"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Smartphone màn hình gập 7.6 inch, chống nước IPX8, khung titan.", false, "FoldX Premium", 45990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000035"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Smartphone tầm trung tốt nhất, Dimensity 7300, camera 108MP, pin 6000mAh.", false, "MidRange Hero A55", 9490000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000036"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Vòng tay thể thao đo 24 chỉ số sức khỏe, pin 21 ngày, chống nước 5ATM.", false, "WristBand Fit Pro", 2490000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000037"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Tablet trẻ em 8 inch, bảo vệ mắt EyeShield+, khóa ứng dụng theo độ tuổi.", false, "AirTab Kids 8", 4990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000038"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Pin dự phòng 25000mAh, sạc 140W, 3 cổng ra, hiển thị số dung lượng.", false, "PowerBank Turbo 25K", 1990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000039"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Kính AR thế hệ đầu, hiển thị thông tin ngay trên kính, kết nối BT 5.3.", false, "SmartGlasses AR-1", 15990000m, null },
                    { new Guid("d0000000-0000-0000-0000-000000000040"), new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Trạm sạc không dây 8 thiết bị cùng lúc, 100W tổng, chân đế sang trọng.", false, "ChargeStation 8-in-1", 3490000m, null }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                columns: new[] { "Address", "AvatarUrl", "Email", "PhoneNumber", "SavedAddresses" },
                values: new object[] { "123 iLuminaty HQ, TP.HCM", null, "admin@iluminaty.com", "0901234567", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c"),
                columns: new[] { "AvatarUrl", "Email", "PhoneNumber", "SavedAddresses" },
                values: new object[] { null, "staff@iluminaty.com", "0907654321", null });

            migrationBuilder.InsertData(
                table: "Inventories",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "ProductId", "ReservedQuantity", "StockQuantity", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("e0000000-0000-0000-0000-000000000001"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000001"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000002"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000003"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000003"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000004"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000005"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000005"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000006"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000007"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000007"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000008"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000008"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000009"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000009"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000010"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000010"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000011"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000011"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000012"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000012"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000013"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000013"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000014"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000014"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000015"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000015"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000016"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000016"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000017"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000017"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000018"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000018"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000019"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000019"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000020"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000020"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000021"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000021"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000022"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000022"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000023"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000023"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000024"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000024"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000025"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000025"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000026"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000026"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000027"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000027"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000028"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000028"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000029"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000029"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000030"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000030"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000031"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000031"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000032"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000032"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000033"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000033"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000034"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000034"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000035"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000035"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000036"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000036"), 0, 5, null },
                    { new Guid("e0000000-0000-0000-0000-000000000037"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000037"), 0, 15, null },
                    { new Guid("e0000000-0000-0000-0000-000000000038"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000038"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000039"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000039"), 0, 30, null },
                    { new Guid("e0000000-0000-0000-0000-000000000040"), new DateTime(2026, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), false, new Guid("d0000000-0000-0000-0000-000000000040"), 0, 15, null }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "CreatedAt", "IsDeleted", "OrderId", "ProductId", "Quantity", "UnitPrice", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("f1000000-0000-0000-0000-000000000001"), new DateTime(2026, 6, 15, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000001"), new Guid("d0000000-0000-0000-0000-000000000001"), 1, 52490000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000002"), new DateTime(2026, 6, 16, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000002"), new Guid("d0000000-0000-0000-0000-000000000001"), 1, 52490000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000003"), new DateTime(2026, 6, 17, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000003"), new Guid("d0000000-0000-0000-0000-000000000011"), 2, 4725000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000004"), new DateTime(2026, 6, 18, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000004"), new Guid("d0000000-0000-0000-0000-000000000011"), 1, 4725000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000005"), new DateTime(2026, 6, 19, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000005"), new Guid("d0000000-0000-0000-0000-000000000021"), 1, 6225000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000006"), new DateTime(2026, 6, 20, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000006"), new Guid("d0000000-0000-0000-0000-000000000021"), 2, 6225000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000007"), new DateTime(2026, 6, 21, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000007"), new Guid("d0000000-0000-0000-0000-000000000032"), 1, 28990000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000008"), new DateTime(2026, 6, 22, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000008"), new Guid("d0000000-0000-0000-0000-000000000032"), 1, 28990000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000009"), new DateTime(2026, 6, 23, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000009"), new Guid("d0000000-0000-0000-0000-000000000035"), 2, 9490000m, null },
                    { new Guid("f1000000-0000-0000-0000-000000000010"), new DateTime(2026, 6, 24, 8, 0, 0, 0, DateTimeKind.Utc), false, new Guid("f0000000-0000-0000-0000-000000000010"), new Guid("d0000000-0000-0000-0000-000000000035"), 1, 9490000m, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Products_ProductId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Inventories",
                keyColumn: "Id",
                keyValue: new Guid("e0000000-0000-0000-0000-000000000010"));

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
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("f1000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("f0000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d0000000-0000-0000-0000-000000000010"));

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

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SavedAddresses",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RecipientName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RecipientPhone",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"),
                column: "Description",
                value: "Laptops and PCs");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"),
                column: "Description",
                value: "Gaming Gear & Peripherals");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"),
                column: "Description",
                value: "Premium Audio Equipment");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"),
                column: "Description",
                value: "Smartphones and Mobile devices");

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                columns: new[] { "Address", "Email", "PhoneNumber" },
                values: new object[] { null, "admin@ecommerce.com", null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c"),
                columns: new[] { "Email", "PhoneNumber" },
                values: new object[] { "staff@ecommerce.com", null });

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
    }
}
