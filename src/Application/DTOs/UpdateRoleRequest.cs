namespace Application.DTOs;

public class UpdateRoleRequest
{
    public string Email { get; set; } = string.Empty;
    public string NewRole { get; set; } = string.Empty; // "Staff" hoặc "Admin"
}