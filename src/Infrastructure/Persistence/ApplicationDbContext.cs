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
    public DbSet<GoodsReceipt> GoodsReceipts => Set<GoodsReceipt>();
    public DbSet<GoodsReceiptDetail> GoodsReceiptDetails => Set<GoodsReceiptDetail>();
    public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();
    
    // CMS
    public DbSet<Banner> Banners => Set<Banner>();
    public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
    
    // Return & Refund
    public DbSet<ReturnRequest> ReturnRequests => Set<ReturnRequest>();
    
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

        // GoodsReceipt Configuration
        modelBuilder.Entity<GoodsReceipt>()
            .HasOne(r => r.CreatedByUser)
            .WithMany()
            .HasForeignKey(r => r.CreatedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        // ReturnRequest Configuration
        modelBuilder.Entity<ReturnRequest>()
            .HasOne(r => r.Order)
            .WithMany()
            .HasForeignKey(r => r.OrderId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<GoodsReceipt>()
            .HasOne(r => r.ApprovedByUser)
            .WithMany()
            .HasForeignKey(r => r.ApprovedByUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<GoodsReceiptDetail>()
            .HasOne(d => d.GoodsReceipt)
            .WithMany(r => r.Details)
            .HasForeignKey(d => d.GoodsReceiptId);

        modelBuilder.Entity<GoodsReceiptDetail>()
            .Property(d => d.ImportPrice)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<GoodsReceipt>()
            .Property(r => r.TotalAmount)
            .HasColumnType("decimal(18,2)");

        // Voucher & Wishlist Configuration
        modelBuilder.Entity<WishlistItem>()
            .HasIndex(w => new { w.UserId, w.ProductId })
            .IsUnique();

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Voucher)
            .WithMany()
            .HasForeignKey(o => o.VoucherId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Voucher>()
            .HasIndex(v => v.Code)
            .IsUnique();

        modelBuilder.Entity<Voucher>()
            .Property(v => v.DiscountValue)
            .HasColumnType("decimal(18,2)");
            
        modelBuilder.Entity<Voucher>()
            .Property(v => v.MinOrderValue)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Voucher>()
            .Property(v => v.MaxDiscountValue)
            .HasColumnType("decimal(18,2)");
            
        modelBuilder.Entity<User>()
            .Property(u => u.TotalSpent)
            .HasColumnType("decimal(18,2)");

        // ==================== USERS ====================
        var adminId = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d");
        var staffId = Guid.Parse("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c");

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                Email = "admin@ecommerce.com",
                FullName = "System Administrator",
                PasswordHash = "$2b$11$kZ/0UcwxKA/eXzbvAnhS9OaGA.l5bQDSxd/lNqcNh5w3xhNeByN/a", // Admin@123
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
                PasswordHash = "$2b$11$kZ/0UcwxKA/eXzbvAnhS9OaGA.l5bQDSxd/lNqcNh5w3xhNeByN/a", // Admin@123
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
        var catAccessoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65");

        var seedDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = catLaptopId, Name = "Laptops",     Description = "Laptops & Máy tính xách tay cao cấp", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = catGamingId, Name = "Gaming",      Description = "Gaming Gear & Phụ kiện Gaming",        CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = catAudioId,  Name = "Audio",       Description = "Thiết bị Âm thanh Cao cấp",            CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = catSmartId,  Name = "Smartphones", Description = "Smartphone & Thiết bị Di động",        CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = catAccessoryId, Name = "Accessories", Description = "Phụ kiện Điện thoại & Sạc",        CreatedAt = seedDate, IsDeleted = false }
        );


        // ==================== PRODUCTS (PREMIUM) ====================
        var p1Id = Guid.Parse("d0000000-0000-0000-0000-000000000001");
        var p2Id = Guid.Parse("d0000000-0000-0000-0000-000000000002");
        var p3Id = Guid.Parse("d0000000-0000-0000-0000-000000000003");
        var p4Id = Guid.Parse("d0000000-0000-0000-0000-000000000004");
        var p5Id = Guid.Parse("d0000000-0000-0000-0000-000000000005");
        var p6Id = Guid.Parse("d0000000-0000-0000-0000-000000000006");
        var p7Id = Guid.Parse("d0000000-0000-0000-0000-000000000007");
        var p8Id = Guid.Parse("d0000000-0000-0000-0000-000000000008");
        var p9Id = Guid.Parse("d0000000-0000-0000-0000-000000000009");
        var p10Id = Guid.Parse("d0000000-0000-0000-0000-000000000010");

        var products = new List<Product>
        {
            new() { 
                Id = p1Id, Name = "MacBook Pro 16 M3 Max", Brand = "Apple", 
                Description = "Siêu phẩm laptop đồ họa cao cấp nhất từ Apple với chip M3 Max 16-core CPU, 40-core GPU. Màn hình Liquid Retina XDR 120Hz siêu sắc nét. Phù hợp cho dân thiết kế 3D, dựng phim chuyên nghiệp.", 
                Price = 89990000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/latop/images (1).jpg", "/image/latop/images (2).jpg", "/image/latop/images (3).jpg" }
            },
            new() { 
                Id = p2Id, Name = "Asus ROG Strix SCAR 18", Brand = "Asus", 
                Description = "Quái thú gaming đích thực với cấu hình khủng Intel Core i9-14900HX, RTX 4090 16GB. Màn hình 18 inch ROG Nebula HDR 240Hz. Hệ thống tản nhiệt thông minh Tri-Fan.", 
                Price = 95000000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/latop/images (4).jpg", "/image/latop/images (5).jpg", "/image/latop/images (6).jpg" }
            },
            new() { 
                Id = p3Id, Name = "iPhone 15 Pro Max", Brand = "Apple", 
                Description = "Điện thoại flagship cao cấp với khung Titanium siêu nhẹ, siêu bền. Chip A17 Pro mạnh mẽ cho trải nghiệm gaming mượt mà, hệ thống camera telephoto 5x độc quyền.", 
                Price = 29990000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/phone/images (1).jpg", "/image/phone/images (2).jpg", "/image/phone/images (3).jpg" }
            },
            new() { 
                Id = p4Id, Name = "Samsung Galaxy S24 Ultra", Brand = "Samsung", 
                Description = "Điện thoại AI đầu tiên thế giới với tính năng Galaxy AI đột phá. Bút S-Pen tích hợp, khung viền Titanium, camera 200MP siêu zoom 100x cực nét.", 
                Price = 32490000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/phone/images (4).jpg", "/image/phone/images (5).jpg", "/image/phone/images (6).jpg" }
            },
            new() {
                Id = p5Id, Name = "Logitech G915 TKL Wireless", Brand = "Logitech",
                Description = "Bàn phím cơ không dây siêu mỏng, trang bị switch GL Low Profile cho tốc độ phản hồi cực nhanh. Hệ thống LED RGB LIGHTSYNC rực rỡ và thời lượng pin cực khủng.",
                Price = 3790000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/keyboard/images (1).jpg", "/image/keyboard/images (2).jpg" }
            },
            new() {
                Id = p6Id, Name = "Razer BlackWidow V4 Pro", Brand = "Razer",
                Description = "Bàn phím cơ full-size với hệ thống LED RGB Chroma rực rỡ dưới viền, trang bị Macro keys và núm xoay Command Dial đa năng.",
                Price = 5990000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/keyboard/images (3).jpg", "/image/keyboard/images (4).jpg" }
            },
            new() {
                Id = p7Id, Name = "Sony SRS-XV800 Wireless", Brand = "Sony",
                Description = "Loa Bluetooth không dây công suất lớn với âm thanh 360 độ cực đỉnh. Tích hợp đèn LED đa sắc và cổng cắm micro hát karaoke chuyên nghiệp.",
                Price = 14490000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/speaker/images (1).jpg", "/image/speaker/images (2).jpg" }
            },
            new() {
                Id = p8Id, Name = "JBL Charge 5", Brand = "JBL",
                Description = "Loa di động chống nước chuẩn IP67 với âm thanh Original Pro Sound cực mạnh. Thời lượng pin 20 tiếng, tích hợp sạc dự phòng vô cùng tiện lợi.",
                Price = 3490000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/speaker/images (3).jpg", "/image/speaker/images (4).jpg" }
            },
            new() {
                Id = p9Id, Name = "Anker 735 Charger (GaNPrime 65W)", Brand = "Anker",
                Description = "Củ sạc siêu nhanh 65W với công nghệ GaN thế hệ mới. Trang bị 2 cổng USB-C và 1 cổng USB-A, thiết kế siêu nhỏ gọn và an toàn tuyệt đối.",
                Price = 1290000, CategoryId = catAccessoryId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/charger/images (2).jpg", "/image/charger/images (3).jpg" }
            },
            new() {
                Id = p10Id, Name = "Spigen Tough Armor Case iPhone 15", Brand = "Spigen",
                Description = "Ốp lưng siêu chống sốc với công nghệ Air Cushion độc quyền. Thiết kế 2 lớp bảo vệ cực tốt kèm chân chống tiện lợi cho việc xem video.",
                Price = 850000, CategoryId = catAccessoryId, CreatedAt = seedDate, IsDeleted = false,
                ImageUrls = new List<string> { "/image/phone case/images (1).jpg", "/image/phone case/images (2).jpg" }
            }
        };

        modelBuilder.Entity<Product>().HasData(products);

        // ==================== INVENTORIES ====================
        var inventories = new List<Inventory>
        {
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000001"), ProductId = p1Id, StockQuantity = 25, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000002"), ProductId = p2Id, StockQuantity = 10, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000003"), ProductId = p3Id, StockQuantity = 50, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000004"), ProductId = p4Id, StockQuantity = 45, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000005"), ProductId = p5Id, StockQuantity = 100, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000006"), ProductId = p6Id, StockQuantity = 30, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000007"), ProductId = p7Id, StockQuantity = 40, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000008"), ProductId = p8Id, StockQuantity = 80, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000009"), ProductId = p9Id, StockQuantity = 200, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false },
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000010"), ProductId = p10Id, StockQuantity = 150, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false }
        };
        modelBuilder.Entity<Inventory>().HasData(inventories);

        // ==================== PRODUCT VARIANTS ====================
        var v1Id = Guid.Parse("a0000000-0000-0000-0000-000000000001");
        var v2Id = Guid.Parse("a0000000-0000-0000-0000-000000000002");
        var v3Id = Guid.Parse("a0000000-0000-0000-0000-000000000003");
        var v4Id = Guid.Parse("a0000000-0000-0000-0000-000000000004");
        var v5Id = Guid.Parse("a0000000-0000-0000-0000-000000000005");
        var v6Id = Guid.Parse("a0000000-0000-0000-0000-000000000006");
        var v7Id = Guid.Parse("a0000000-0000-0000-0000-000000000007");
        var v8Id = Guid.Parse("a0000000-0000-0000-0000-000000000008");
        var v9Id = Guid.Parse("a0000000-0000-0000-0000-000000000009");
        var v10Id = Guid.Parse("a0000000-0000-0000-0000-000000000010");
        var v11Id = Guid.Parse("a0000000-0000-0000-0000-000000000011");

        var variants = new List<ProductVariant>
        {
            new() { Id = v1Id, ProductId = p1Id, Sku = "MAC-M3M-16-1TB-SLV", Name = "36GB RAM / 1TB SSD / Silver", AttributesJson = "{\"ram\":\"36GB Unified\",\"storage\":\"1TB SSD\",\"color\":\"Silver\",\"cpu\":\"M3 Max 14-core\",\"gpu\":\"30-core GPU\"}", Price = 89990000, StockQuantity = 10, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v2Id, ProductId = p1Id, Sku = "MAC-M3M-16-1TB-BLK", Name = "36GB RAM / 1TB SSD / Space Black", AttributesJson = "{\"ram\":\"36GB Unified\",\"storage\":\"1TB SSD\",\"color\":\"Space Black\",\"cpu\":\"M3 Max 14-core\",\"gpu\":\"30-core GPU\"}", Price = 89990000, StockQuantity = 5, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v3Id, ProductId = p2Id, Sku = "ASUS-SCAR18-4090", Name = "64GB RAM / 2TB SSD / RTX 4090", AttributesJson = "{\"ram\":\"64GB DDR5\",\"storage\":\"2TB SSD Gen4\",\"color\":\"Off Black\",\"cpu\":\"Intel Core i9-14900HX\",\"gpu\":\"NVIDIA RTX 4090 16GB\"}", Price = 95000000, StockQuantity = 3, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v4Id, ProductId = p3Id, Sku = "IP15PM-256-NAT", Name = "256GB / Natural Titanium", AttributesJson = "{\"storage\":\"256GB\",\"color\":\"Natural Titanium\",\"screen\":\"6.7 inch Super Retina XDR\"}", Price = 29990000, StockQuantity = 20, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v5Id, ProductId = p3Id, Sku = "IP15PM-512-WHT", Name = "512GB / White Titanium", AttributesJson = "{\"storage\":\"512GB\",\"color\":\"White Titanium\",\"screen\":\"6.7 inch Super Retina XDR\"}", Price = 34990000, StockQuantity = 15, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v6Id, ProductId = p4Id, Sku = "S24U-512-BLK", Name = "12GB RAM / 512GB / Titanium Black", AttributesJson = "{\"ram\":\"12GB\",\"storage\":\"512GB\",\"color\":\"Titanium Black\",\"cpu\":\"Snapdragon 8 Gen 3 for Galaxy\"}", Price = 32490000, StockQuantity = 12, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v7Id, ProductId = p5Id, Sku = "LOGI-G915-TKL", Name = "GL Tactile / Black", AttributesJson = "{\"switch\":\"GL Tactile\",\"color\":\"Black\",\"connection\":\"Lightspeed Wireless\"}", Price = 3790000, StockQuantity = 30, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v8Id, ProductId = p6Id, Sku = "RAZER-BW-V4", Name = "Green Switch / Black", AttributesJson = "{\"switch\":\"Razer Green\",\"color\":\"Black\",\"connection\":\"Wired\"}", Price = 5990000, StockQuantity = 25, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v9Id, ProductId = p7Id, Sku = "SONY-XV800", Name = "Standard Edition", AttributesJson = "{\"color\":\"Black\",\"connection\":\"Bluetooth 5.2\"}", Price = 14490000, StockQuantity = 10, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v10Id, ProductId = p8Id, Sku = "JBL-CHG5-BLK", Name = "Black", AttributesJson = "{\"color\":\"Black\",\"battery\":\"20 Hours\"}", Price = 3490000, StockQuantity = 50, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v11Id, ProductId = p9Id, Sku = "ANKER-735-BLK", Name = "65W / Black", AttributesJson = "{\"color\":\"Black\",\"power\":\"65W\",\"ports\":\"2C 1A\"}", Price = 1290000, StockQuantity = 100, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate }
        };

        modelBuilder.Entity<ProductVariant>().HasData(variants);

        // ==================== SEED ORDERS ====================
        var o1Id = Guid.Parse("f0000000-0000-0000-0000-000000000001");
        var oi1Id = Guid.Parse("f1000000-0000-0000-0000-000000000001");

        // ==================== PRODUCT REVIEWS ====================
        modelBuilder.Entity<ProductReview>().HasData(
            new ProductReview { Id = Guid.Parse("70000000-0000-0000-0000-000000000001"), ProductId = p1Id, UserId = adminId, OrderId = o1Id, Rating = 5, Comment = "Máy siêu mạnh, render video 4K nhanh như chớp. Màn hình đẹp xuất sắc, pin trâu dùng cả ngày không hết.", CreatedAt = seedDate },
            new ProductReview { Id = Guid.Parse("70000000-0000-0000-0000-000000000002"), ProductId = p1Id, UserId = staffId, OrderId = o1Id, Rating = 4, Comment = "Màu Space Black cực kỳ sang trọng, ít bám vân tay hơn. Bàn phím gõ êm nhưng giá hơi chát.", CreatedAt = seedDate.AddDays(1) },
            new ProductReview { Id = Guid.Parse("70000000-0000-0000-0000-000000000003"), ProductId = p3Id, UserId = adminId, OrderId = o1Id, Rating = 5, Comment = "Cầm rất nhẹ so với đời 14 Pro Max. Camera zoom 5x chụp ảnh chân dung cực kỳ sắc nét. Quá tuyệt vời!", CreatedAt = seedDate.AddDays(2) },
            new ProductReview { Id = Guid.Parse("70000000-0000-0000-0000-000000000004"), ProductId = p4Id, UserId = staffId, OrderId = o1Id, Rating = 5, Comment = "Tính năng phiên dịch trực tiếp AI cực kỳ hữu ích khi đi du lịch. Màn hình chống chói rất ngon.", CreatedAt = seedDate.AddDays(3) }
        );

        modelBuilder.Entity<Order>().HasData(new Order
        {
            Id = o1Id,
            UserId = adminId,
            Status = "Delivered",
            TotalAmount = 29990000,
            ShippingAddress = "Số 10 Nguyễn Huệ, Quận 1, TP.HCM",
            RecipientName = "Nguyễn Văn An",
            RecipientPhone = "0901234567",
            PaymentMethod = "MoMo",
            CreatedAt = seedDate.AddDays(10),
            IsDeleted = false
        });

        modelBuilder.Entity<OrderItem>().HasData(new OrderItem
        {
            Id = oi1Id,
            OrderId = o1Id,
            ProductId = p3Id,
            ProductVariantId = v4Id,
            Quantity = 1,
            UnitPrice = 29990000,
            CreatedAt = seedDate.AddDays(10),
            IsDeleted = false
        });

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
