namespace Application.DTOs;

public class UpdateProfileRequest
{
    public string? FullName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? AvatarUrl { get; set; }
    public string? SavedAddresses { get; set; } // JSON array of strings
}
