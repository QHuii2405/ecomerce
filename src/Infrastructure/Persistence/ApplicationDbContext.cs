using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Khai báo các bảng dữ liệu 
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Inventory> Inventories => Set<Inventory>();
    public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
    public DbSet<ProductReview> ProductReviews => Set<ProductReview>();
    public DbSet<PaymentTransaction> PaymentTransactions => Set<PaymentTransaction>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<JobPosting> JobPostings => Set<JobPosting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cấu hình Quan hệ 1-1 giữa Product và Inventory 
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Inventory)
            .WithOne(i => i.Product)
            .HasForeignKey<Inventory>(i => i.ProductId);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Product>()
            .HasMany(p => p.Variants)
            .WithOne(v => v.Product)
            .HasForeignKey(v => v.ProductId);

        modelBuilder.Entity<ProductReview>()
            .HasIndex(r => new { r.ProductId, r.UserId })
            .IsUnique();

        modelBuilder.Entity<ProductReview>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId);

        modelBuilder.Entity<ProductReview>()
            .HasOne(r => r.User)
            .WithMany()
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ProductReview>()
            .HasOne(r => r.Order)
            .WithMany(o => o.Reviews)
            .HasForeignKey(r => r.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OrderItem>()
            .HasOne(i => i.ProductVariant)
            .WithMany()
            .HasForeignKey(i => i.ProductVariantId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<PaymentTransaction>()
            .HasOne(p => p.Order)
            .WithMany(o => o.PaymentTransactions)
            .HasForeignKey(p => p.OrderId);

        // ==================== USERS ====================
        var adminId = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d");
        var staffId = Guid.Parse("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                Email = "admin@iluminaty.com",
                FullName = "System Administrator",
                PasswordHash = "$2a$11$Le3WZPxhWsgyNrsgQ5oEEOQ6uCYXNMpEWZ14rRqQAkZejS75R/zJK",
                Role = "Admin",
                PhoneNumber = "0901234567",
                Address = "123 iLuminaty HQ, TP.HCM",
                CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new User
            {
                Id = staffId,
                Email = "staff@iluminaty.com",
                FullName = "Store Operator",
                PasswordHash = "$2a$11$9Nm0pHWKVQg31ja3mv6en.MwVYy2fEp11Tn0kqATnBJIehM2iL/Ky",
                Role = "Staff",
                PhoneNumber = "0907654321",
                CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            }
        );

        // ==================== CATEGORIES ====================
        var catLaptopId    = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61");
        var catGamingId    = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62");
        var catAudioId     = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63");
        var catSmartId     = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64");

        var seedDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = catLaptopId, Name = "Laptops",     Description = "Laptops & Máy tính xách tay cao cấp", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = catGamingId, Name = "Gaming",      Description = "Gaming Gear & Phụ kiện Gaming",        CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = catAudioId,  Name = "Audio",       Description = "Thiết bị Âm thanh Cao cấp",            CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = catSmartId,  Name = "Smartphones", Description = "Smartphone & Thiết bị Di động",        CreatedAt = seedDate, IsDeleted = false }
        );

        // ==================== PRODUCTS — LAPTOPS (10) ====================
        var products = new List<Product>
        {
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000001"), Name = "iLuminaty Pro X1",        Description = "Flagship laptop với Intel Core Ultra 9, RAM 32GB, màn OLED 4K, siêu mỏng 14mm.", Price = 52_490_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000002"), Name = "iLuminaty Air 15",        Description = "Laptop văn phòng 15 inch nhẹ 1.2kg, pin 20 giờ, AMD Ryzen 7 8845HS.", Price = 28_990_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000003"), Name = "Phantom Studio Pro",      Description = "Workstation sáng tạo với RTX 4090, RAM 64GB ECC, thiết kế CNC aluminium.", Price = 89_900_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000004"), Name = "Nexus Book S13",          Description = "Ultrabook 13 inch gọn nhẹ, màn hình 2.8K AMOLED 120Hz, Intel Arc GPU.", Price = 22_590_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000005"), Name = "Lumina Elite X Pro",      Description = "Business laptop bảo mật cấp cao, TPM 2.0, khuôn mặt IR, RAM 16GB LPDDR5X.", Price = 35_990_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000006"), Name = "ProVision 16 Max",        Description = "Laptop đồ họa chuyên dụng 16 inch, RTX 4070, bàn phím cơ Cherry MX.", Price = 45_990_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000007"), Name = "Swift Creator X",         Description = "Creator laptop màn 4K OLED touch, Ryzen 9 8945HX, card đồ họa Radeon 890M.", Price = 38_500_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000008"), Name = "Nova Slim 14",            Description = "Mỏng nhẹ nhất phân khúc, chỉ 0.9kg, màn 14 inch QHD+, pin 18 giờ.", Price = 19_990_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000009"), Name = "Titan Gamer Pro",         Description = "Gaming laptop 17 inch, RTX 4080 12GB, màn 240Hz QHD, tản nhiệt vapor chamber.", Price = 58_990_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000010"), Name = "CodeBook Developer Ed.", Description = "Laptop cho lập trình viên, Linux ready, 32GB RAM, SSD 2TB NVMe PCIe 5.0.", Price = 31_990_000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false },

            // ==================== GAMING (10) ====================
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000011"), Name = "Ghost Mechanical Keyboard", Description = "Bàn phím cơ không dây đỉnh cao, switch êm ái, đèn nền RGB per-key.", Price = 4_725_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000012"), Name = "Phantom Mouse Pro X",      Description = "Chuột gaming 26000 DPI, sensor quang học, trọng lượng 58g siêu nhẹ.", Price = 2_990_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000013"), Name = "HyperPad XL RGB",          Description = "Mousepad gaming khổng lồ 900×400mm, bề mặt tốc độ cao, viền LED RGB.", Price = 890_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000014"), Name = "Storm Gaming Chair",       Description = "Ghế gaming ergonomic, đệm memory foam, tựa đầu 4D, chịu tải 150kg.", Price = 8_990_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000015"), Name = "NitroView 27\" 165Hz",    Description = "Màn hình gaming 27 inch QHD 165Hz, 1ms GTG, FreeSync Premium, HDR400.", Price = 9_990_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000016"), Name = "StreamDeck Pro",           Description = "Bảng điều khiển stream 32 phím LCD, tích hợp OBS, Twitch, Discord.", Price = 3_490_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000017"), Name = "VortexKeys 60%",           Description = "Bàn phím compact 60%, hot-swap socket, keycap PBT double-shot.", Price = 2_190_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000018"), Name = "CoreController Pro",       Description = "Tay cầm chơi game PC/Mobile, kết nối USB-C + Bluetooth, trigger haptic.", Price = 1_990_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000019"), Name = "UltraCapture 4K Cam",     Description = "Webcam gaming 4K 30fps, AI background removal, ring light LED tích hợp.", Price = 3_290_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000020"), Name = "Aurora Gaming Headset",   Description = "Headset gaming 7.1 surround, mic khử ồn AI, đệm tai foam ngủ ngon.", Price = 2_490_000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false },

            // ==================== AUDIO (10) ====================
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000021"), Name = "Sonic Buds Pro",          Description = "Tai nghe chống ồn ANC thế hệ 3, driver 11mm, pin 36 giờ kèm case.", Price = 6_225_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000022"), Name = "CrystalAir Studio Mic",   Description = "Micro condenser USB-C cho studio, cardioid/omnidirectional, 192kHz.", Price = 4_590_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000023"), Name = "BassBoom Speaker 360",    Description = "Loa Bluetooth 360 độ IPX7, bass mạnh, pin 24 giờ, kết nối đa điểm.", Price = 3_190_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000024"), Name = "Lumina Over-Ear H1",      Description = "Tai nghe over-ear cao cấp, driver planar 50mm, DAC tích hợp 32-bit.", Price = 12_990_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000025"), Name = "AudioBar Pro 2.1",        Description = "Thanh loa soundbar 2.1 kèm subwoofer, Dolby Atmos, HDMI ARC.", Price = 8_490_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000026"), Name = "VoiceLink Pro Earbuds",   Description = "TWS earbuds cho công việc, mic MEMS 4 array, ENC khử ồn, pin 28 giờ.", Price = 3_990_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000027"), Name = "PocketDAC Ultra",         Description = "DAC/Amp di động, ES9038Q2M chip, power 300mW vào 32Ω, USB-C.", Price = 5_290_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000028"), Name = "SonicWave IEM S5",        Description = "In-ear monitor 5 driver hybrid, cáp bạc 8 lõi, dải tần 8Hz-45kHz.", Price = 9_990_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000029"), Name = "ClearCast Podcast Kit",   Description = "Bộ podcast: mic arm + mic + pop filter + audio interface USB.", Price = 4_290_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000030"), Name = "HomeAudio Amp X3",        Description = "Amplifier hi-fi cho loa passive, 2×100W class AB, Bluetooth 5.3.", Price = 6_990_000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false },

            // ==================== SMARTPHONES (10) ====================
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000031"), Name = "Nexus Watch S",           Description = "Đồng hồ thông minh AMOLED 1.8\", đo SpO2/ECG/BP, GPS độc lập.", Price = 9_975_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000032"), Name = "iLuminaty Phone Pro",     Description = "Flagship phone Snapdragon 8 Elite, camera 200MP, sạc 100W, IP68.", Price = 28_990_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000033"), Name = "NovaPad Ultra 12",        Description = "Tablet 12 inch 2.8K 144Hz, M2 chip, 5G, bút stylus thế hệ 2.", Price = 22_490_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000034"), Name = "FoldX Premium",           Description = "Smartphone màn hình gập 7.6 inch, chống nước IPX8, khung titan.", Price = 45_990_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000035"), Name = "MidRange Hero A55",       Description = "Smartphone tầm trung tốt nhất, Dimensity 7300, camera 108MP, pin 6000mAh.", Price = 9_490_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000036"), Name = "WristBand Fit Pro",       Description = "Vòng tay thể thao đo 24 chỉ số sức khỏe, pin 21 ngày, chống nước 5ATM.", Price = 2_490_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000037"), Name = "AirTab Kids 8",           Description = "Tablet trẻ em 8 inch, bảo vệ mắt EyeShield+, khóa ứng dụng theo độ tuổi.", Price = 4_990_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000038"), Name = "PowerBank Turbo 25K",     Description = "Pin dự phòng 25000mAh, sạc 140W, 3 cổng ra, hiển thị số dung lượng.", Price = 1_990_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000039"), Name = "SmartGlasses AR-1",       Description = "Kính AR thế hệ đầu, hiển thị thông tin ngay trên kính, kết nối BT 5.3.", Price = 15_990_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("d0000000-0000-0000-0000-000000000040"), Name = "ChargeStation 8-in-1",    Description = "Trạm sạc không dây 8 thiết bị cùng lúc, 100W tổng, chân đế sang trọng.", Price = 3_490_000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false },
        };

        modelBuilder.Entity<Product>().HasData(products);

        // ==================== INVENTORIES ====================
        var inventories = products.Select((p, i) => new Inventory
        {
            Id = Guid.Parse($"e0000000-0000-0000-0000-{(i + 1):D12}"),
            ProductId = p.Id,
            StockQuantity = (i % 5 == 0) ? 5 : (i % 3 == 0) ? 15 : 30,
            ReservedQuantity = 0,
            CreatedAt = seedDate,
            IsDeleted = false
        }).ToList();

        modelBuilder.Entity<Inventory>().HasData(inventories);

        // ==================== SEED ORDERS (10) ====================
        var p1Id = Guid.Parse("d0000000-0000-0000-0000-000000000001");
        var p11Id = Guid.Parse("d0000000-0000-0000-0000-000000000011");
        var p21Id = Guid.Parse("d0000000-0000-0000-0000-000000000021");
        var p31Id = Guid.Parse("d0000000-0000-0000-0000-000000000032");
        var p35Id = Guid.Parse("d0000000-0000-0000-0000-000000000035");

        var orderStatuses = new[] { "Pending", "Pending", "Pending", "Confirmed", "Confirmed", "Shipping", "Shipping", "Delivered", "Delivered", "Cancelled" };

        for (int i = 1; i <= 10; i++)
        {
            var orderId = Guid.Parse($"f0000000-0000-0000-0000-{i:D12}");
            var orderItemId = Guid.Parse($"f1000000-0000-0000-0000-{i:D12}");
            var productId = i <= 2 ? p1Id : i <= 4 ? p11Id : i <= 6 ? p21Id : i <= 8 ? p31Id : p35Id;
            var unitPrice = i <= 2 ? 52490000m : i <= 4 ? 4725000m : i <= 6 ? 6225000m : i <= 8 ? 28990000m : 9490000m;
            var qty = (i % 3 == 0) ? 2 : 1;

            modelBuilder.Entity<Order>().HasData(new Order
            {
                Id = orderId,
                UserId = adminId,
                Status = orderStatuses[i - 1],
                TotalAmount = unitPrice * qty,
                ShippingAddress = $"Số {i * 10} Nguyễn Huệ, Quận 1, TP.HCM",
                RecipientName = i % 2 == 0 ? "Nguyễn Văn An" : "Trần Thị Bình",
                RecipientPhone = $"090{i:D7}",
                PaymentMethod = i % 3 == 0 ? "MoMo" : i % 3 == 1 ? "COD" : "VietQR",
                Note = i == 10 ? "Hủy do thay đổi kế hoạch" : null,
                CreatedAt = new DateTime(2026, 6, i + 14, 8, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            });

            modelBuilder.Entity<OrderItem>().HasData(new OrderItem
            {
                Id = orderItemId,
                OrderId = orderId,
                ProductId = productId,
                Quantity = qty,
                UnitPrice = unitPrice,
                CreatedAt = new DateTime(2026, 6, i + 14, 8, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            });
        }

        // ==================== JOB POSTINGS (Tuyển dụng) ====================
        modelBuilder.Entity<JobPosting>().HasData(
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000001"),
                Title = "Nhân Viên Bán Hàng Công Nghệ",
                Department = "Kinh Doanh",
                Location = "TP. Hồ Chí Minh",
                EmploymentType = "Toàn thời gian",
                SalaryRange = "12 - 18 triệu VNĐ + Hoa hồng",
                Description = "Tư vấn và bán các sản phẩm công nghệ cao cấp tại showroom iLuminaty Shop. Hỗ trợ khách hàng chọn sản phẩm phù hợp, xử lý đơn hàng và chăm sóc sau bán.",
                Requirements = "• Tốt nghiệp CĐ/ĐH các ngành liên quan\n• Yêu thích công nghệ, am hiểu sản phẩm điện tử\n• Kỹ năng giao tiếp và thuyết trình tốt\n• Ưu tiên có kinh nghiệm bán lẻ công nghệ",
                Benefits = "• Lương cứng + hoa hồng hấp dẫn\n• Bảo hiểm đầy đủ theo luật\n• Giảm giá 30% sản phẩm nội bộ\n• Đào tạo sản phẩm hàng tháng",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 15, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000002"),
                Title = "Lập Trình Viên Full-Stack (.NET + React)",
                Department = "Công Nghệ",
                Location = "TP. Hồ Chí Minh / Remote",
                EmploymentType = "Toàn thời gian",
                SalaryRange = "25 - 45 triệu VNĐ",
                Description = "Phát triển và bảo trì nền tảng thương mại điện tử iLuminaty Shop. Xây dựng API .NET, giao diện React, tích hợp thanh toán và quản lý kho.",
                Requirements = "• 2+ năm kinh nghiệm .NET Core / ASP.NET\n• Thành thạo React, TypeScript/JavaScript\n• Hiểu biết SQL Server, Redis, REST API\n• Có kinh nghiệm Clean Architecture là lợi thế",
                Benefits = "• Môi trường startup năng động\n• Làm việc linh hoạt (hybrid/remote)\n• MacBook Pro + màn hình 4K\n• Thưởng dự án theo quý",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 20, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000003"),
                Title = "Chuyên Viên Marketing Digital",
                Department = "Marketing",
                Location = "TP. Hồ Chí Minh",
                EmploymentType = "Toàn thời gian",
                SalaryRange = "15 - 22 triệu VNĐ",
                Description = "Lên kế hoạch và triển khai chiến dịch marketing online cho iLuminaty Shop. Quản lý social media, chạy quảng cáo Facebook/Google, phân tích hiệu quả.",
                Requirements = "• 1+ năm kinh nghiệm marketing digital\n• Thành thạo Facebook Ads, Google Ads\n• Kỹ năng viết content và thiết kế cơ bản\n• Hiểu biết về ngành công nghệ/điện tử",
                Benefits = "• Ngân sách marketing thử nghiệm\n• Tham gia sự kiện công nghệ lớn\n• Team trẻ, sáng tạo\n• Review lương 2 lần/năm",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 25, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            },
            new JobPosting
            {
                Id = Guid.Parse("b1000000-0000-0000-0000-000000000004"),
                Title = "Thực Tập Sinh Kho & Logistics",
                Department = "Vận Hành",
                Location = "TP. Hồ Chí Minh",
                EmploymentType = "Thực tập",
                SalaryRange = "5 - 7 triệu VNĐ + Phụ cấp",
                Description = "Hỗ trợ quản lý kho hàng, nhập xuất sản phẩm, đóng gói và theo dõi vận chuyển. Cơ hội học hỏi quy trình logistics thương mại điện tử.",
                Requirements = "• Sinh viên năm 3-4 các ngành QTKD, Logistics\n• Cẩn thận, trách nhiệm cao\n• Biết sử dụng Excel cơ bản\n• Có thể làm việc ít nhất 4 tháng",
                Benefits = "• Cơ hội chính thức hóa sau thực tập\n• Được đào tạo hệ thống quản lý kho\n• Môi trường chuyên nghiệp\n• Phụ cấp ăn trưa",
                IsActive = true,
                PostedAt = new DateTime(2026, 6, 28, 0, 0, 0, DateTimeKind.Utc),
                CreatedAt = seedDate,
                IsDeleted = false
            }
        );
    }
}