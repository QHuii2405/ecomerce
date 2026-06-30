using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest request);
    Task<TokenResponse> LoginAsync(LoginRequest request);
    Task<TokenResponse> RefreshTokenAsync(TokenRequest request);
    Task<bool> RevokeTokenAsync(string email);
    Task<string> UpdateRoleAsync(UpdateRoleRequest request);
    Task<User> GetCurrentUserAsync(Guid userId);
    Task<bool> UpdateProfileAsync(UpdateProfileRequest request, Guid userId);
    Task<IEnumerable<User>> GetAllUsersAsync();
}
