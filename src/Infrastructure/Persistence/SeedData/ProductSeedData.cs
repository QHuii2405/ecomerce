using Domain.Entities;

namespace Infrastructure.Persistence.SeedData;

public static class ProductSeedData
{
    public static (IEnumerable<Product> Products, IEnumerable<Inventory> Inventories, IEnumerable<ProductVariant> Variants) GetSeedData()
    {
        var products = new List<Product>();
        var inventories = new List<Inventory>();
        var variants = new List<ProductVariant>();
        
        var seedDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc);
        

        // --- Product 1 ---
        var p1Id = Guid.Parse("d0000000-0000-0000-0000-000000000001");
        products.Add(new Product { 
            Id = p1Id, 
            Name = "Apple Premium Laptops Model 1", 
            Brand = "Apple", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 20600000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (1).jpg", "/image/latop/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000001"), 
            ProductId = p1Id, 
            StockQuantity = 39, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000001"), 
            ProductId = p1Id, 
            Sku = "APP-1-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 20600000, 
            StockQuantity = 5, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000002"), 
            ProductId = p1Id, 
            Sku = "APP-1-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 21600000, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 2 ---
        var p2Id = Guid.Parse("d0000000-0000-0000-0000-000000000002");
        products.Add(new Product { 
            Id = p2Id, 
            Name = "Asus Premium Laptops Model 2", 
            Brand = "Asus", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Asus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 10500000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (11).jpg", "/image/latop/images (12).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000002"), 
            ProductId = p2Id, 
            StockQuantity = 59, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000003"), 
            ProductId = p2Id, 
            Sku = "ASU-2-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 10500000, 
            StockQuantity = 23, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 3 ---
        var p3Id = Guid.Parse("d0000000-0000-0000-0000-000000000003");
        products.Add(new Product { 
            Id = p3Id, 
            Name = "Dell Premium Laptops Model 3", 
            Brand = "Dell", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Dell. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 8000000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (13).jpg", "/image/latop/images (14).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000003"), 
            ProductId = p3Id, 
            StockQuantity = 33, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000004"), 
            ProductId = p3Id, 
            Sku = "DEL-3-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 8000000, 
            StockQuantity = 7, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000005"), 
            ProductId = p3Id, 
            Sku = "DEL-3-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 9000000, 
            StockQuantity = 11, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 4 ---
        var p4Id = Guid.Parse("d0000000-0000-0000-0000-000000000004");
        products.Add(new Product { 
            Id = p4Id, 
            Name = "HP Premium Laptops Model 4", 
            Brand = "HP", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu HP. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 2700000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (15).jpg", "/image/latop/images (16).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000004"), 
            ProductId = p4Id, 
            StockQuantity = 31, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000006"), 
            ProductId = p4Id, 
            Sku = "HP-4-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 2700000, 
            StockQuantity = 17, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 5 ---
        var p5Id = Guid.Parse("d0000000-0000-0000-0000-000000000005");
        products.Add(new Product { 
            Id = p5Id, 
            Name = "Lenovo Premium Laptops Model 5", 
            Brand = "Lenovo", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Lenovo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 11600000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (17).jpg", "/image/latop/images (2).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000005"), 
            ProductId = p5Id, 
            StockQuantity = 58, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000007"), 
            ProductId = p5Id, 
            Sku = "LEN-5-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 11600000, 
            StockQuantity = 23, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000008"), 
            ProductId = p5Id, 
            Sku = "LEN-5-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 12600000, 
            StockQuantity = 17, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 6 ---
        var p6Id = Guid.Parse("d0000000-0000-0000-0000-000000000006");
        products.Add(new Product { 
            Id = p6Id, 
            Name = "Apple Premium Laptops Model 6", 
            Brand = "Apple", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 12500000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (3).jpg", "/image/latop/images (4).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000006"), 
            ProductId = p6Id, 
            StockQuantity = 52, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000009"), 
            ProductId = p6Id, 
            Sku = "APP-6-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 12500000, 
            StockQuantity = 23, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 7 ---
        var p7Id = Guid.Parse("d0000000-0000-0000-0000-000000000007");
        products.Add(new Product { 
            Id = p7Id, 
            Name = "Asus Premium Laptops Model 7", 
            Brand = "Asus", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Asus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 1000000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (5).jpg", "/image/latop/images (6).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000007"), 
            ProductId = p7Id, 
            StockQuantity = 18, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000010"), 
            ProductId = p7Id, 
            Sku = "ASU-7-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 1000000, 
            StockQuantity = 12, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000011"), 
            ProductId = p7Id, 
            Sku = "ASU-7-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 2000000, 
            StockQuantity = 8, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 8 ---
        var p8Id = Guid.Parse("d0000000-0000-0000-0000-000000000008");
        products.Add(new Product { 
            Id = p8Id, 
            Name = "Dell Premium Laptops Model 8", 
            Brand = "Dell", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Dell. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 20900000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (7).jpg", "/image/latop/images (8).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000008"), 
            ProductId = p8Id, 
            StockQuantity = 16, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000012"), 
            ProductId = p8Id, 
            Sku = "DEL-8-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 20900000, 
            StockQuantity = 10, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 9 ---
        var p9Id = Guid.Parse("d0000000-0000-0000-0000-000000000009");
        products.Add(new Product { 
            Id = p9Id, 
            Name = "HP Premium Laptops Model 9", 
            Brand = "HP", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu HP. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 12900000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (9).jpg", "/image/latop/images.jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000009"), 
            ProductId = p9Id, 
            StockQuantity = 36, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000013"), 
            ProductId = p9Id, 
            Sku = "HP-9-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 12900000, 
            StockQuantity = 20, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000014"), 
            ProductId = p9Id, 
            Sku = "HP-9-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 13900000, 
            StockQuantity = 21, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 10 ---
        var p10Id = Guid.Parse("d0000000-0000-0000-0000-000000000010");
        products.Add(new Product { 
            Id = p10Id, 
            Name = "Lenovo Premium Laptops Model 10", 
            Brand = "Lenovo", 
            Description = "Đây là sản phẩm Laptops cao cấp từ thương hiệu Lenovo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 7300000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/latop/images (1).jpg", "/image/latop/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000010"), 
            ProductId = p10Id, 
            StockQuantity = 47, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000015"), 
            ProductId = p10Id, 
            Sku = "LEN-10-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 7300000, 
            StockQuantity = 16, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 11 ---
        var p11Id = Guid.Parse("d0000000-0000-0000-0000-000000000011");
        products.Add(new Product { 
            Id = p11Id, 
            Name = "Apple Premium Smartphones Model 1", 
            Brand = "Apple", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 4600000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (1).jpg", "/image/phone/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000011"), 
            ProductId = p11Id, 
            StockQuantity = 36, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000016"), 
            ProductId = p11Id, 
            Sku = "APP-11-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 4600000, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000017"), 
            ProductId = p11Id, 
            Sku = "APP-11-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 5600000, 
            StockQuantity = 10, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 12 ---
        var p12Id = Guid.Parse("d0000000-0000-0000-0000-000000000012");
        products.Add(new Product { 
            Id = p12Id, 
            Name = "Samsung Premium Smartphones Model 2", 
            Brand = "Samsung", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Samsung. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 15800000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (11).jpg", "/image/phone/images (12).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000012"), 
            ProductId = p12Id, 
            StockQuantity = 50, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000018"), 
            ProductId = p12Id, 
            Sku = "SAM-12-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 15800000, 
            StockQuantity = 6, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 13 ---
        var p13Id = Guid.Parse("d0000000-0000-0000-0000-000000000013");
        products.Add(new Product { 
            Id = p13Id, 
            Name = "Xiaomi Premium Smartphones Model 3", 
            Brand = "Xiaomi", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Xiaomi. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 12500000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (13).jpg", "/image/phone/images (14).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000013"), 
            ProductId = p13Id, 
            StockQuantity = 42, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000019"), 
            ProductId = p13Id, 
            Sku = "XIA-13-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 12500000, 
            StockQuantity = 9, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000020"), 
            ProductId = p13Id, 
            Sku = "XIA-13-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 13500000, 
            StockQuantity = 22, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 14 ---
        var p14Id = Guid.Parse("d0000000-0000-0000-0000-000000000014");
        products.Add(new Product { 
            Id = p14Id, 
            Name = "Oppo Premium Smartphones Model 4", 
            Brand = "Oppo", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Oppo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 17200000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (15).jpg", "/image/phone/images (16).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000014"), 
            ProductId = p14Id, 
            StockQuantity = 48, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000021"), 
            ProductId = p14Id, 
            Sku = "OPP-14-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 17200000, 
            StockQuantity = 7, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 15 ---
        var p15Id = Guid.Parse("d0000000-0000-0000-0000-000000000015");
        products.Add(new Product { 
            Id = p15Id, 
            Name = "Vivo Premium Smartphones Model 5", 
            Brand = "Vivo", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Vivo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 17700000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (18).jpg", "/image/phone/images (2).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000015"), 
            ProductId = p15Id, 
            StockQuantity = 19, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000022"), 
            ProductId = p15Id, 
            Sku = "VIV-15-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 17700000, 
            StockQuantity = 20, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000023"), 
            ProductId = p15Id, 
            Sku = "VIV-15-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 18700000, 
            StockQuantity = 23, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 16 ---
        var p16Id = Guid.Parse("d0000000-0000-0000-0000-000000000016");
        products.Add(new Product { 
            Id = p16Id, 
            Name = "Apple Premium Smartphones Model 6", 
            Brand = "Apple", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Apple. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 1300000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (3).jpg", "/image/phone/images (4).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000016"), 
            ProductId = p16Id, 
            StockQuantity = 46, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000024"), 
            ProductId = p16Id, 
            Sku = "APP-16-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 1300000, 
            StockQuantity = 8, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 17 ---
        var p17Id = Guid.Parse("d0000000-0000-0000-0000-000000000017");
        products.Add(new Product { 
            Id = p17Id, 
            Name = "Samsung Premium Smartphones Model 7", 
            Brand = "Samsung", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Samsung. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 9700000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (5).jpg", "/image/phone/images (6).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000017"), 
            ProductId = p17Id, 
            StockQuantity = 29, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000025"), 
            ProductId = p17Id, 
            Sku = "SAM-17-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 9700000, 
            StockQuantity = 24, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000026"), 
            ProductId = p17Id, 
            Sku = "SAM-17-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 10700000, 
            StockQuantity = 20, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 18 ---
        var p18Id = Guid.Parse("d0000000-0000-0000-0000-000000000018");
        products.Add(new Product { 
            Id = p18Id, 
            Name = "Xiaomi Premium Smartphones Model 8", 
            Brand = "Xiaomi", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Xiaomi. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 11100000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (7).jpg", "/image/phone/images (8).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000018"), 
            ProductId = p18Id, 
            StockQuantity = 18, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000027"), 
            ProductId = p18Id, 
            Sku = "XIA-18-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 11100000, 
            StockQuantity = 24, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 19 ---
        var p19Id = Guid.Parse("d0000000-0000-0000-0000-000000000019");
        products.Add(new Product { 
            Id = p19Id, 
            Name = "Oppo Premium Smartphones Model 9", 
            Brand = "Oppo", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Oppo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 12300000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (9).jpg", "/image/phone/images.jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000019"), 
            ProductId = p19Id, 
            StockQuantity = 31, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000028"), 
            ProductId = p19Id, 
            Sku = "OPP-19-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 12300000, 
            StockQuantity = 23, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000029"), 
            ProductId = p19Id, 
            Sku = "OPP-19-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 13300000, 
            StockQuantity = 19, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 20 ---
        var p20Id = Guid.Parse("d0000000-0000-0000-0000-000000000020");
        products.Add(new Product { 
            Id = p20Id, 
            Name = "Vivo Premium Smartphones Model 10", 
            Brand = "Vivo", 
            Description = "Đây là sản phẩm Smartphones cao cấp từ thương hiệu Vivo. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 6800000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone/images (1).jpg", "/image/phone/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000020"), 
            ProductId = p20Id, 
            StockQuantity = 49, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000030"), 
            ProductId = p20Id, 
            Sku = "VIV-20-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 6800000, 
            StockQuantity = 5, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 21 ---
        var p21Id = Guid.Parse("d0000000-0000-0000-0000-000000000021");
        products.Add(new Product { 
            Id = p21Id, 
            Name = "Logitech Premium Gaming Model 1", 
            Brand = "Logitech", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu Logitech. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 13200000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (1).jpg", "/image/keyboard/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000021"), 
            ProductId = p21Id, 
            StockQuantity = 49, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000031"), 
            ProductId = p21Id, 
            Sku = "LOG-21-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 13200000, 
            StockQuantity = 7, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000032"), 
            ProductId = p21Id, 
            Sku = "LOG-21-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 14200000, 
            StockQuantity = 12, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 22 ---
        var p22Id = Guid.Parse("d0000000-0000-0000-0000-000000000022");
        products.Add(new Product { 
            Id = p22Id, 
            Name = "Razer Premium Gaming Model 2", 
            Brand = "Razer", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu Razer. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 12100000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (11).jpg", "/image/keyboard/images (12).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000022"), 
            ProductId = p22Id, 
            StockQuantity = 56, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000033"), 
            ProductId = p22Id, 
            Sku = "RAZ-22-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 12100000, 
            StockQuantity = 10, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 23 ---
        var p23Id = Guid.Parse("d0000000-0000-0000-0000-000000000023");
        products.Add(new Product { 
            Id = p23Id, 
            Name = "Corsair Premium Gaming Model 3", 
            Brand = "Corsair", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu Corsair. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 14600000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (13).jpg", "/image/keyboard/images (14).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000023"), 
            ProductId = p23Id, 
            StockQuantity = 34, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000034"), 
            ProductId = p23Id, 
            Sku = "COR-23-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 14600000, 
            StockQuantity = 11, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000035"), 
            ProductId = p23Id, 
            Sku = "COR-23-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 15600000, 
            StockQuantity = 24, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 24 ---
        var p24Id = Guid.Parse("d0000000-0000-0000-0000-000000000024");
        products.Add(new Product { 
            Id = p24Id, 
            Name = "SteelSeries Premium Gaming Model 4", 
            Brand = "SteelSeries", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu SteelSeries. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 17000000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (15).jpg", "/image/keyboard/images (16).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000024"), 
            ProductId = p24Id, 
            StockQuantity = 51, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000036"), 
            ProductId = p24Id, 
            Sku = "STE-24-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 17000000, 
            StockQuantity = 20, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 25 ---
        var p25Id = Guid.Parse("d0000000-0000-0000-0000-000000000025");
        products.Add(new Product { 
            Id = p25Id, 
            Name = "HyperX Premium Gaming Model 5", 
            Brand = "HyperX", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu HyperX. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 1400000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (17).jpg", "/image/keyboard/images (18).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000025"), 
            ProductId = p25Id, 
            StockQuantity = 32, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000037"), 
            ProductId = p25Id, 
            Sku = "HYP-25-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 1400000, 
            StockQuantity = 11, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000038"), 
            ProductId = p25Id, 
            Sku = "HYP-25-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 2400000, 
            StockQuantity = 12, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 26 ---
        var p26Id = Guid.Parse("d0000000-0000-0000-0000-000000000026");
        products.Add(new Product { 
            Id = p26Id, 
            Name = "Logitech Premium Gaming Model 6", 
            Brand = "Logitech", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu Logitech. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 17000000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (2).jpg", "/image/keyboard/images (3).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000026"), 
            ProductId = p26Id, 
            StockQuantity = 58, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000039"), 
            ProductId = p26Id, 
            Sku = "LOG-26-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 17000000, 
            StockQuantity = 12, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 27 ---
        var p27Id = Guid.Parse("d0000000-0000-0000-0000-000000000027");
        products.Add(new Product { 
            Id = p27Id, 
            Name = "Razer Premium Gaming Model 7", 
            Brand = "Razer", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu Razer. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 8300000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (4).jpg", "/image/keyboard/images (5).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000027"), 
            ProductId = p27Id, 
            StockQuantity = 56, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000040"), 
            ProductId = p27Id, 
            Sku = "RAZ-27-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 8300000, 
            StockQuantity = 22, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000041"), 
            ProductId = p27Id, 
            Sku = "RAZ-27-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 9300000, 
            StockQuantity = 17, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 28 ---
        var p28Id = Guid.Parse("d0000000-0000-0000-0000-000000000028");
        products.Add(new Product { 
            Id = p28Id, 
            Name = "Corsair Premium Gaming Model 8", 
            Brand = "Corsair", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu Corsair. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 17400000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (6).jpg", "/image/keyboard/images (7).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000028"), 
            ProductId = p28Id, 
            StockQuantity = 51, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000042"), 
            ProductId = p28Id, 
            Sku = "COR-28-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 17400000, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 29 ---
        var p29Id = Guid.Parse("d0000000-0000-0000-0000-000000000029");
        products.Add(new Product { 
            Id = p29Id, 
            Name = "SteelSeries Premium Gaming Model 9", 
            Brand = "SteelSeries", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu SteelSeries. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 16400000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images (8).jpg", "/image/keyboard/images (9).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000029"), 
            ProductId = p29Id, 
            StockQuantity = 33, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000043"), 
            ProductId = p29Id, 
            Sku = "STE-29-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 16400000, 
            StockQuantity = 23, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000044"), 
            ProductId = p29Id, 
            Sku = "STE-29-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 17400000, 
            StockQuantity = 18, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 30 ---
        var p30Id = Guid.Parse("d0000000-0000-0000-0000-000000000030");
        products.Add(new Product { 
            Id = p30Id, 
            Name = "HyperX Premium Gaming Model 10", 
            Brand = "HyperX", 
            Description = "Đây là sản phẩm Gaming cao cấp từ thương hiệu HyperX. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 15400000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/keyboard/images.jpg", "/image/keyboard/tải xuống (1).webp" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000030"), 
            ProductId = p30Id, 
            StockQuantity = 12, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000045"), 
            ProductId = p30Id, 
            Sku = "HYP-30-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 15400000, 
            StockQuantity = 21, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 31 ---
        var p31Id = Guid.Parse("d0000000-0000-0000-0000-000000000031");
        products.Add(new Product { 
            Id = p31Id, 
            Name = "Sony Premium Audio Model 1", 
            Brand = "Sony", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Sony. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 9400000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (1).jpg", "/image/speaker/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000031"), 
            ProductId = p31Id, 
            StockQuantity = 37, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000046"), 
            ProductId = p31Id, 
            Sku = "SON-31-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 9400000, 
            StockQuantity = 6, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000047"), 
            ProductId = p31Id, 
            Sku = "SON-31-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 10400000, 
            StockQuantity = 22, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 32 ---
        var p32Id = Guid.Parse("d0000000-0000-0000-0000-000000000032");
        products.Add(new Product { 
            Id = p32Id, 
            Name = "JBL Premium Audio Model 2", 
            Brand = "JBL", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu JBL. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 9500000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (11).jpg", "/image/speaker/images (12).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000032"), 
            ProductId = p32Id, 
            StockQuantity = 12, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000048"), 
            ProductId = p32Id, 
            Sku = "JBL-32-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 9500000, 
            StockQuantity = 11, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 33 ---
        var p33Id = Guid.Parse("d0000000-0000-0000-0000-000000000033");
        products.Add(new Product { 
            Id = p33Id, 
            Name = "Bose Premium Audio Model 3", 
            Brand = "Bose", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Bose. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 14100000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (13).jpg", "/image/speaker/images (14).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000033"), 
            ProductId = p33Id, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000049"), 
            ProductId = p33Id, 
            Sku = "BOS-33-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 14100000, 
            StockQuantity = 14, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000050"), 
            ProductId = p33Id, 
            Sku = "BOS-33-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 15100000, 
            StockQuantity = 20, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 34 ---
        var p34Id = Guid.Parse("d0000000-0000-0000-0000-000000000034");
        products.Add(new Product { 
            Id = p34Id, 
            Name = "Marshall Premium Audio Model 4", 
            Brand = "Marshall", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Marshall. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 16700000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (15).jpg", "/image/speaker/images (16).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000034"), 
            ProductId = p34Id, 
            StockQuantity = 48, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000051"), 
            ProductId = p34Id, 
            Sku = "MAR-34-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 16700000, 
            StockQuantity = 14, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 35 ---
        var p35Id = Guid.Parse("d0000000-0000-0000-0000-000000000035");
        products.Add(new Product { 
            Id = p35Id, 
            Name = "Sennheiser Premium Audio Model 5", 
            Brand = "Sennheiser", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Sennheiser. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 1600000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (19).jpg", "/image/speaker/images (2).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000035"), 
            ProductId = p35Id, 
            StockQuantity = 54, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000052"), 
            ProductId = p35Id, 
            Sku = "SEN-35-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 1600000, 
            StockQuantity = 14, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000053"), 
            ProductId = p35Id, 
            Sku = "SEN-35-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 2600000, 
            StockQuantity = 5, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 36 ---
        var p36Id = Guid.Parse("d0000000-0000-0000-0000-000000000036");
        products.Add(new Product { 
            Id = p36Id, 
            Name = "Sony Premium Audio Model 6", 
            Brand = "Sony", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Sony. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 10300000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (3).jpg", "/image/speaker/images (4).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000036"), 
            ProductId = p36Id, 
            StockQuantity = 48, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000054"), 
            ProductId = p36Id, 
            Sku = "SON-36-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 10300000, 
            StockQuantity = 24, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 37 ---
        var p37Id = Guid.Parse("d0000000-0000-0000-0000-000000000037");
        products.Add(new Product { 
            Id = p37Id, 
            Name = "JBL Premium Audio Model 7", 
            Brand = "JBL", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu JBL. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 10400000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (5).jpg", "/image/speaker/images (6).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000037"), 
            ProductId = p37Id, 
            StockQuantity = 58, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000055"), 
            ProductId = p37Id, 
            Sku = "JBL-37-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 10400000, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000056"), 
            ProductId = p37Id, 
            Sku = "JBL-37-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 11400000, 
            StockQuantity = 8, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 38 ---
        var p38Id = Guid.Parse("d0000000-0000-0000-0000-000000000038");
        products.Add(new Product { 
            Id = p38Id, 
            Name = "Bose Premium Audio Model 8", 
            Brand = "Bose", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Bose. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 17800000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (7).jpg", "/image/speaker/images (8).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000038"), 
            ProductId = p38Id, 
            StockQuantity = 55, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000057"), 
            ProductId = p38Id, 
            Sku = "BOS-38-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 17800000, 
            StockQuantity = 16, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 39 ---
        var p39Id = Guid.Parse("d0000000-0000-0000-0000-000000000039");
        products.Add(new Product { 
            Id = p39Id, 
            Name = "Marshall Premium Audio Model 9", 
            Brand = "Marshall", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Marshall. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 2700000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (9).jpg", "/image/speaker/images.jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000039"), 
            ProductId = p39Id, 
            StockQuantity = 39, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000058"), 
            ProductId = p39Id, 
            Sku = "MAR-39-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 2700000, 
            StockQuantity = 5, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000059"), 
            ProductId = p39Id, 
            Sku = "MAR-39-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 3700000, 
            StockQuantity = 24, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 40 ---
        var p40Id = Guid.Parse("d0000000-0000-0000-0000-000000000040");
        products.Add(new Product { 
            Id = p40Id, 
            Name = "Sennheiser Premium Audio Model 10", 
            Brand = "Sennheiser", 
            Description = "Đây là sản phẩm Audio cao cấp từ thương hiệu Sennheiser. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 7600000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/speaker/images (1).jpg", "/image/speaker/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000040"), 
            ProductId = p40Id, 
            StockQuantity = 24, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000060"), 
            ProductId = p40Id, 
            Sku = "SEN-40-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 7600000, 
            StockQuantity = 17, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 41 ---
        var p41Id = Guid.Parse("d0000000-0000-0000-0000-000000000041");
        products.Add(new Product { 
            Id = p41Id, 
            Name = "Spigen Premium Accessories Model 1", 
            Brand = "Spigen", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Spigen. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 5200000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (1).jpg", "/image/phone case/images (10).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000041"), 
            ProductId = p41Id, 
            StockQuantity = 49, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000061"), 
            ProductId = p41Id, 
            Sku = "SPI-41-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 5200000, 
            StockQuantity = 20, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000062"), 
            ProductId = p41Id, 
            Sku = "SPI-41-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 6200000, 
            StockQuantity = 12, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 42 ---
        var p42Id = Guid.Parse("d0000000-0000-0000-0000-000000000042");
        products.Add(new Product { 
            Id = p42Id, 
            Name = "Anker Premium Accessories Model 2", 
            Brand = "Anker", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Anker. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 1400000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (11).jpg", "/image/phone case/images (12).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000042"), 
            ProductId = p42Id, 
            StockQuantity = 40, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000063"), 
            ProductId = p42Id, 
            Sku = "ANK-42-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 1400000, 
            StockQuantity = 10, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 43 ---
        var p43Id = Guid.Parse("d0000000-0000-0000-0000-000000000043");
        products.Add(new Product { 
            Id = p43Id, 
            Name = "UAG Premium Accessories Model 3", 
            Brand = "UAG", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu UAG. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 8500000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (13).jpg", "/image/phone case/images (2).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000043"), 
            ProductId = p43Id, 
            StockQuantity = 50, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000064"), 
            ProductId = p43Id, 
            Sku = "UAG-43-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 8500000, 
            StockQuantity = 6, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000065"), 
            ProductId = p43Id, 
            Sku = "UAG-43-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 9500000, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 44 ---
        var p44Id = Guid.Parse("d0000000-0000-0000-0000-000000000044");
        products.Add(new Product { 
            Id = p44Id, 
            Name = "Nillkin Premium Accessories Model 4", 
            Brand = "Nillkin", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Nillkin. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 10700000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (26).jpg", "/image/phone case/images (3).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000044"), 
            ProductId = p44Id, 
            StockQuantity = 59, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000066"), 
            ProductId = p44Id, 
            Sku = "NIL-44-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 10700000, 
            StockQuantity = 21, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 45 ---
        var p45Id = Guid.Parse("d0000000-0000-0000-0000-000000000045");
        products.Add(new Product { 
            Id = p45Id, 
            Name = "Baseus Premium Accessories Model 5", 
            Brand = "Baseus", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Baseus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 10600000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (4).jpg", "/image/phone case/images (5).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000045"), 
            ProductId = p45Id, 
            StockQuantity = 21, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000067"), 
            ProductId = p45Id, 
            Sku = "BAS-45-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 10600000, 
            StockQuantity = 21, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000068"), 
            ProductId = p45Id, 
            Sku = "BAS-45-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 11600000, 
            StockQuantity = 21, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 46 ---
        var p46Id = Guid.Parse("d0000000-0000-0000-0000-000000000046");
        products.Add(new Product { 
            Id = p46Id, 
            Name = "Spigen Premium Accessories Model 6", 
            Brand = "Spigen", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Spigen. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 6000000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (6).jpg", "/image/phone case/images (7).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000046"), 
            ProductId = p46Id, 
            StockQuantity = 28, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000069"), 
            ProductId = p46Id, 
            Sku = "SPI-46-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 6000000, 
            StockQuantity = 17, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 47 ---
        var p47Id = Guid.Parse("d0000000-0000-0000-0000-000000000047");
        products.Add(new Product { 
            Id = p47Id, 
            Name = "Anker Premium Accessories Model 7", 
            Brand = "Anker", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Anker. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 20300000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (8).jpg", "/image/phone case/images (9).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000047"), 
            ProductId = p47Id, 
            StockQuantity = 45, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000070"), 
            ProductId = p47Id, 
            Sku = "ANK-47-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 20300000, 
            StockQuantity = 16, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000071"), 
            ProductId = p47Id, 
            Sku = "ANK-47-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 21300000, 
            StockQuantity = 7, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 48 ---
        var p48Id = Guid.Parse("d0000000-0000-0000-0000-000000000048");
        products.Add(new Product { 
            Id = p48Id, 
            Name = "UAG Premium Accessories Model 8", 
            Brand = "UAG", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu UAG. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 9500000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images.jpg", "/image/phone case/images (1).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000048"), 
            ProductId = p48Id, 
            StockQuantity = 29, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000072"), 
            ProductId = p48Id, 
            Sku = "UAG-48-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 9500000, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 49 ---
        var p49Id = Guid.Parse("d0000000-0000-0000-0000-000000000049");
        products.Add(new Product { 
            Id = p49Id, 
            Name = "Nillkin Premium Accessories Model 9", 
            Brand = "Nillkin", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Nillkin. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 12700000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (10).jpg", "/image/phone case/images (11).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000049"), 
            ProductId = p49Id, 
            StockQuantity = 45, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000073"), 
            ProductId = p49Id, 
            Sku = "NIL-49-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 12700000, 
            StockQuantity = 8, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000074"), 
            ProductId = p49Id, 
            Sku = "NIL-49-1", 
            Name = "Phiên bản Cao cấp", 
            AttributesJson = "{\"color\":\"White\"}", 
            Price = 13700000, 
            StockQuantity = 11, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        // --- Product 50 ---
        var p50Id = Guid.Parse("d0000000-0000-0000-0000-000000000050");
        products.Add(new Product { 
            Id = p50Id, 
            Name = "Baseus Premium Accessories Model 10", 
            Brand = "Baseus", 
            Description = "Đây là sản phẩm Accessories cao cấp từ thương hiệu Baseus. Thiết kế sang trọng, hiệu năng vượt trội, phù hợp cho mọi nhu cầu sử dụng.", 
            Price = 11900000, 
            CategoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65"), 
            CreatedAt = seedDate, 
            IsDeleted = false,
            ImageUrls = new List<string> { "/image/phone case/images (12).jpg", "/image/phone case/images (13).jpg" }
        });
        
        inventories.Add(new Inventory { 
            Id = Guid.Parse("e0000000-0000-0000-0000-000000000050"), 
            ProductId = p50Id, 
            StockQuantity = 47, 
            ReservedQuantity = 0, 
            CreatedAt = seedDate, 
            IsDeleted = false 
        });
        
        variants.Add(new ProductVariant { 
            Id = Guid.Parse("a0000000-0000-0000-0000-000000000075"), 
            ProductId = p50Id, 
            Sku = "BAS-50-0", 
            Name = "Phiên bản Tiêu chuẩn", 
            AttributesJson = "{\"color\":\"Black\"}", 
            Price = 11900000, 
            StockQuantity = 15, 
            ReservedQuantity = 0, 
            IsActive = true, 
            CreatedAt = seedDate 
        });
        return (products, inventories, variants);
    }
}
