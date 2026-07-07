using Domain.Entities;

namespace Infrastructure.Persistence.SeedData;

public static class CategorySeedData
{
    public static readonly Guid CatLaptopId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c61");
    public static readonly Guid CatGamingId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c62");
    public static readonly Guid CatAudioId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c63");
    public static readonly Guid CatSmartId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c64");
    public static readonly Guid CatAccessoryId = Guid.Parse("c1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c65");

    public static IEnumerable<Category> GetCategories()
    {
        var seedDate = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc);
        return new List<Category>
        {
            new Category { Id = CatLaptopId, Name = "Laptops", Description = "Laptops & Máy tính xách tay cao cấp", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = CatGamingId, Name = "Gaming", Description = "Gaming Gear & Phụ kiện Gaming", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = CatAudioId, Name = "Audio", Description = "Thiết bị Âm thanh Cao cấp", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = CatSmartId, Name = "Smartphones", Description = "Smartphone & Thiết bị Di động", CreatedAt = seedDate, IsDeleted = false },
            new Category { Id = CatAccessoryId, Name = "Accessories", Description = "Phụ kiện Điện thoại & Sạc", CreatedAt = seedDate, IsDeleted = false }
        };
    }
}
