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
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cấu hình Quan hệ 1-1 giữa Product và Inventory 
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Inventory)
            .WithOne(i => i.Product)
            .HasForeignKey<Inventory>(i => i.ProductId);

        // Gieo dữ liệu tài khoản mặc định (Data Seeding) với mã băm tĩnh đạt chuẩn Production
        var adminId = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d");
        var staffId = Guid.Parse("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c");

        // Sử dụng các mã băm BCrypt tĩnh được tính toán trước để tránh lỗi thay đổi mô hình liên tục của EF Core
        // Mật khẩu giải mã của Admin: AdminSecure2026@
        // Mật khẩu giải mã của Staff: StaffSecure2026@
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminId,
                Email = "admin@ecommerce.com",
                FullName = "System Administrator",
                PasswordHash = "$2a$11$Le3WZPxhWsgyNrsgQ5oEEOQ6uCYXNMpEWZ14rRqQAkZejS75R/zJK",
                Role = "Admin",
                CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            },
            new User
            {
                Id = staffId,
                Email = "staff@ecommerce.com",
                FullName = "Store Operator",
                PasswordHash = "$2a$11$9Nm0pHWKVQg31ja3mv6en.MwVYy2fEp11Tn0kqATnBJIehM2iL/Ky",
                Role = "Staff",
                CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            }
        );

        // Seeding Categories, Products, and Inventories of Lumina Tech
        var catLaptopId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61");
        var catPeripheralId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62");
        var catAudioId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63");
        var catSmartphoneId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64");

        var prodLaptopId = Guid.Parse("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c71");
        var prodKeyboardId = Guid.Parse("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c72");
        var prodAudioId = Guid.Parse("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c73");
        var prodWatchId = Guid.Parse("d1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c74");

        var invLaptopId = Guid.Parse("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c81");
        var invKeyboardId = Guid.Parse("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c82");
        var invAudioId = Guid.Parse("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c83");
        var invWatchId = Guid.Parse("b1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c84");

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = catLaptopId, Name = "Laptops", Description = "Laptops and PCs", CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Category { Id = catPeripheralId, Name = "Gaming", Description = "Gaming Gear & Peripherals", CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Category { Id = catAudioId, Name = "Audio", Description = "Premium Audio Equipment", CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Category { Id = catSmartphoneId, Name = "Smartphones", Description = "Smartphones and Mobile devices", CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = prodLaptopId, Name = "Lumina Pro X1", Description = "Sức mạnh tối thượng từ vi xử lý Intel Core Ultra mới nhất, thiết kế siêu mỏng nhẹ sang trọng.", Price = 32490000, CategoryId = catLaptopId, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Product { Id = prodKeyboardId, Name = "Ghost Mechanical Keyboard", Description = "Bàn phím cơ không dây đỉnh cao, switch êm ái, đèn nền RGB cá tính.", Price = 4725000, CategoryId = catPeripheralId, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Product { Id = prodAudioId, Name = "Sonic Buds Pro", Description = "Tai nghe chống ồn chủ động vượt trội, âm thanh độ phân giải cao sắc nét.", Price = 6225000, CategoryId = catAudioId, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Product { Id = prodWatchId, Name = "Nexus Watch S", Description = "Đồng hồ thông minh thế hệ mới tích hợp đo chỉ số sức khỏe chuyên sâu.", Price = 9975000, CategoryId = catSmartphoneId, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
        );

        modelBuilder.Entity<Inventory>().HasData(
            new Inventory { Id = invLaptopId, ProductId = prodLaptopId, StockQuantity = 10, ReservedQuantity = 0, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Inventory { Id = invKeyboardId, ProductId = prodKeyboardId, StockQuantity = 25, ReservedQuantity = 0, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Inventory { Id = invAudioId, ProductId = prodAudioId, StockQuantity = 50, ReservedQuantity = 0, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false },
            new Inventory { Id = invWatchId, ProductId = prodWatchId, StockQuantity = 15, ReservedQuantity = 0, CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc), IsDeleted = false }
        );
    }
}