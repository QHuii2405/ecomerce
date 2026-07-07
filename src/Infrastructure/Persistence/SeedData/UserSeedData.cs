using Domain.Entities;

namespace Infrastructure.Persistence.SeedData;

public static class UserSeedData
{
    public static readonly Guid AdminId = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d");
    public static readonly Guid StaffId = Guid.Parse("f6e5d4c3-b2a1-0f9e-8d7c-6b5a4f3e2d1c");

    public static IEnumerable<User> GetUsers()
    {
        return new List<User>
        {
            new User
            {
                Id = AdminId,
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
                Id = StaffId,
                Email = "staff@iluminaty.com",
                FullName = "Store Operator",
                PasswordHash = "$2b$11$kZ/0UcwxKA/eXzbvAnhS9OaGA.l5bQDSxd/lNqcNh5w3xhNeByN/a", // Admin@123
                Role = "Staff",
                PhoneNumber = "0907654321",
                CreatedAt = new DateTime(2026, 6, 24, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false
            }
        };
    }
}
