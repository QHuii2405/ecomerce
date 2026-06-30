using Domain.Common;

namespace Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = "Customer";
    public string? AvatarUrl { get; set; }
    public string? SavedAddresses { get; set; } // JSON: ["Địa chỉ 1","Địa chỉ 2"]

    // Hỗ trợ Refresh Token chuẩn Production
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}