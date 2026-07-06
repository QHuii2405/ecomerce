const fs = require('fs');

const seedData = `
        // ==================== PRODUCTS (PREMIUM) ====================
        var p1Id = Guid.Parse("d0000000-0000-0000-0000-000000000001");
        var p2Id = Guid.Parse("d0000000-0000-0000-0000-000000000002");
        var p3Id = Guid.Parse("d0000000-0000-0000-0000-000000000003");
        var p4Id = Guid.Parse("d0000000-0000-0000-0000-000000000004");
        var p5Id = Guid.Parse("d0000000-0000-0000-0000-000000000005");
        var p6Id = Guid.Parse("d0000000-0000-0000-0000-000000000006");
        var p7Id = Guid.Parse("d0000000-0000-0000-0000-000000000007");
        var p8Id = Guid.Parse("d0000000-0000-0000-0000-000000000008");

        var products = new List<Product>
        {
            new() { 
                Id = p1Id, Name = "MacBook Pro 16 M3 Max", Brand = "Apple", 
                Description = "Siêu phẩm laptop đồ họa cao cấp nhất từ Apple với chip M3 Max 16-core CPU, 40-core GPU. Màn hình Liquid Retina XDR 120Hz siêu sắc nét. Phù hợp cho dân thiết kế 3D, dựng phim chuyên nghiệp.", 
                Price = 89990000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false 
            },
            new() { 
                Id = p2Id, Name = "Asus ROG Strix SCAR 18", Brand = "Asus", 
                Description = "Quái thú gaming đích thực với cấu hình khủng Intel Core i9-14900HX, RTX 4090 16GB. Màn hình 18 inch ROG Nebula HDR 240Hz. Hệ thống tản nhiệt thông minh Tri-Fan.", 
                Price = 95000000, CategoryId = catLaptopId, CreatedAt = seedDate, IsDeleted = false 
            },
            new() { 
                Id = p3Id, Name = "iPhone 15 Pro Max", Brand = "Apple", 
                Description = "Điện thoại flagship cao cấp với khung Titanium siêu nhẹ, siêu bền. Chip A17 Pro mạnh mẽ cho trải nghiệm gaming mượt mà, hệ thống camera telephoto 5x độc quyền.", 
                Price = 29990000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false 
            },
            new() { 
                Id = p4Id, Name = "Samsung Galaxy S24 Ultra", Brand = "Samsung", 
                Description = "Điện thoại AI đầu tiên thế giới với tính năng Galaxy AI đột phá. Bút S-Pen tích hợp, khung viền Titanium, camera 200MP siêu zoom 100x cực nét.", 
                Price = 32490000, CategoryId = catSmartId, CreatedAt = seedDate, IsDeleted = false 
            },
            new() {
                Id = p5Id, Name = "Logitech G Pro X Superlight 2", Brand = "Logitech",
                Description = "Chuột gaming siêu nhẹ chỉ 60g, trang bị switch lai quang học-cơ học LIGHTFORCE và cảm biến HERO 2 cao cấp. Dành riêng cho tuyển thủ eSports.",
                Price = 3790000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false
            },
            new() {
                Id = p6Id, Name = "Razer BlackWidow V4 Pro", Brand = "Razer",
                Description = "Bàn phím cơ full-size với hệ thống LED RGB Chroma rực rỡ dưới viền, trang bị Macro keys và núm xoay Command Dial đa năng.",
                Price = 5990000, CategoryId = catGamingId, CreatedAt = seedDate, IsDeleted = false
            },
            new() {
                Id = p7Id, Name = "Sony WH-1000XM5", Brand = "Sony",
                Description = "Tai nghe Over-ear chống ồn chủ động (ANC) tốt nhất thế giới. Tích hợp 8 micro, màng loa 30mm tinh chỉnh âm thanh High-Resolution Audio.",
                Price = 7490000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false
            },
            new() {
                Id = p8Id, Name = "AirPods Pro 2 (USB-C)", Brand = "Apple",
                Description = "Tai nghe In-ear True Wireless chống ồn đỉnh cao. Chip H2 xử lý âm thanh trong trẻo, chế độ Xuyên âm thông minh, cổng sạc Type-C thế hệ mới.",
                Price = 6190000, CategoryId = catAudioId, CreatedAt = seedDate, IsDeleted = false
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
            new() { Id = Guid.Parse("e0000000-0000-0000-0000-000000000008"), ProductId = p8Id, StockQuantity = 80, ReservedQuantity = 0, CreatedAt = seedDate, IsDeleted = false }
        };
        modelBuilder.Entity<Inventory>().HasData(inventories);

        // ==================== PRODUCT VARIANTS ====================
        var v1Id = Guid.Parse("a0000000-0000-0000-0000-000000000001");
        var v2Id = Guid.Parse("a0000000-0000-0000-0000-000000000002");
        var v3Id = Guid.Parse("a0000000-0000-0000-0000-000000000003");
        var v4Id = Guid.Parse("a0000000-0000-0000-0000-000000000004");
        var v5Id = Guid.Parse("a0000000-0000-0000-0000-000000000005");
        var v6Id = Guid.Parse("a0000000-0000-0000-0000-000000000006");

        var variants = new List<ProductVariant>
        {
            new() { Id = v1Id, ProductId = p1Id, Sku = "MAC-M3M-16-1TB-SLV", Name = "36GB RAM / 1TB SSD / Silver", AttributesJson = "{\\"ram\\":\\"36GB Unified\\",\\"storage\\":\\"1TB SSD\\",\\"color\\":\\"Silver\\",\\"cpu\\":\\"M3 Max 14-core\\",\\"gpu\\":\\"30-core GPU\\"}", Price = 89990000, StockQuantity = 10, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v2Id, ProductId = p1Id, Sku = "MAC-M3M-16-1TB-BLK", Name = "36GB RAM / 1TB SSD / Space Black", AttributesJson = "{\\"ram\\":\\"36GB Unified\\",\\"storage\\":\\"1TB SSD\\",\\"color\\":\\"Space Black\\",\\"cpu\\":\\"M3 Max 14-core\\",\\"gpu\\":\\"30-core GPU\\"}", Price = 89990000, StockQuantity = 5, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v3Id, ProductId = p2Id, Sku = "ASUS-SCAR18-4090", Name = "64GB RAM / 2TB SSD / RTX 4090", AttributesJson = "{\\"ram\\":\\"64GB DDR5\\",\\"storage\\":\\"2TB SSD Gen4\\",\\"color\\":\\"Off Black\\",\\"cpu\\":\\"Intel Core i9-14900HX\\",\\"gpu\\":\\"NVIDIA RTX 4090 16GB\\"}", Price = 95000000, StockQuantity = 3, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v4Id, ProductId = p3Id, Sku = "IP15PM-256-NAT", Name = "256GB / Natural Titanium", AttributesJson = "{\\"storage\\":\\"256GB\\",\\"color\\":\\"Natural Titanium\\",\\"screen\\":\\"6.7 inch Super Retina XDR\\"}", Price = 29990000, StockQuantity = 20, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v5Id, ProductId = p3Id, Sku = "IP15PM-512-WHT", Name = "512GB / White Titanium", AttributesJson = "{\\"storage\\":\\"512GB\\",\\"color\\":\\"White Titanium\\",\\"screen\\":\\"6.7 inch Super Retina XDR\\"}", Price = 34990000, StockQuantity = 15, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate },
            new() { Id = v6Id, ProductId = p4Id, Sku = "S24U-512-BLK", Name = "12GB RAM / 512GB / Titanium Black", AttributesJson = "{\\"ram\\":\\"12GB\\",\\"storage\\":\\"512GB\\",\\"color\\":\\"Titanium Black\\",\\"cpu\\":\\"Snapdragon 8 Gen 3 for Galaxy\\"}", Price = 32490000, StockQuantity = 12, ReservedQuantity = 0, IsActive = true, CreatedAt = seedDate }
        };

        modelBuilder.Entity<ProductVariant>().HasData(variants);

        // ==================== PRODUCT REVIEWS ====================
        modelBuilder.Entity<ProductReview>().HasData(
            new ProductReview { Id = Guid.Parse("r0000000-0000-0000-0000-000000000001"), ProductId = p1Id, UserId = adminId, Rating = 5, Comment = "Máy siêu mạnh, render video 4K nhanh như chớp. Màn hình đẹp xuất sắc, pin trâu dùng cả ngày không hết.", IsApproved = true, CreatedAt = seedDate },
            new ProductReview { Id = Guid.Parse("r0000000-0000-0000-0000-000000000002"), ProductId = p1Id, UserId = staffId, Rating = 4, Comment = "Màu Space Black cực kỳ sang trọng, ít bám vân tay hơn. Bàn phím gõ êm nhưng giá hơi chát.", IsApproved = true, CreatedAt = seedDate.AddDays(1) },
            new ProductReview { Id = Guid.Parse("r0000000-0000-0000-0000-000000000003"), ProductId = p3Id, UserId = adminId, Rating = 5, Comment = "Cầm rất nhẹ so với đời 14 Pro Max. Camera zoom 5x chụp ảnh chân dung cực kỳ sắc nét. Quá tuyệt vời!", IsApproved = true, CreatedAt = seedDate.AddDays(2) },
            new ProductReview { Id = Guid.Parse("r0000000-0000-0000-0000-000000000004"), ProductId = p4Id, UserId = staffId, Rating = 5, Comment = "Tính năng phiên dịch trực tiếp AI cực kỳ hữu ích khi đi du lịch. Màn hình chống chói rất ngon.", IsApproved = true, CreatedAt = seedDate.AddDays(3) }
        );

        // ==================== SEED ORDERS ====================
        var o1Id = Guid.Parse("f0000000-0000-0000-0000-000000000001");
        var oi1Id = Guid.Parse("f1000000-0000-0000-0000-000000000001");

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
`;

let content = fs.readFileSync('d:/Project/ecomerce/src/Infrastructure/Persistence/ApplicationDbContext.cs', 'utf8');
const startIndex = content.indexOf('        // ==================== PRODUCTS — LAPTOPS (10) ====================');
const endIndex = content.indexOf('        // ==================== JOB POSTINGS (Tuyển dụng) ====================');

if (startIndex !== -1 && endIndex !== -1) {
    content = content.substring(0, startIndex) + seedData + '\n' + content.substring(endIndex);
    fs.writeFileSync('d:/Project/ecomerce/src/Infrastructure/Persistence/ApplicationDbContext.cs', content);
    console.log("Replaced seed data successfully");
} else {
    console.log("Could not find start or end index");
}
