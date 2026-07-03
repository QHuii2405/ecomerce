namespace WebAPI.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        try
        {
            var message = await _authService.RegisterAsync(request);
            return Ok(new { Message = message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Đăng ký thất bại: " + ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        try
        {
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Đăng nhập thất bại: " + ex.Message });
        }
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(TokenRequest request)
    {
        try
        {
            var response = await _authService.RefreshTokenAsync(request);
            return Ok(response);
        }
        catch (SecurityTokenException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Làm mới mã token thất bại: " + ex.Message });
        }
    }

    [HttpPost("revoke-token")]
    [Authorize] // Chỉ người dùng đã đăng nhập mới được thu hồi token (đăng xuất)
    public async Task<IActionResult> RevokeToken()
    {
        try
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            if (email == null) return Unauthorized();

            var success = await _authService.RevokeTokenAsync(email);
            if (success)
            {
                return Ok(new { Message = "Đã thu hồi token thành công (Đăng xuất thành công)." });
            }
            return BadRequest(new { Message = "Không thể thu hồi token." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lỗi: " + ex.Message });
        }
    }

    [HttpPost("update-role")]
    [Authorize(Roles = "Admin")] // Chỉ Admin thực sự mới được nâng quyền cho người khác
    public async Task<IActionResult> UpdateRole(UpdateRoleRequest request)
    {
        try
        {
            var message = await _authService.UpdateRoleAsync(request);
            return Ok(new { Message = message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Cập nhật quyền thất bại: " + ex.Message });
        }
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        try
        {
            var userIdVal = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdVal == null) return Unauthorized();

            var userId = Guid.Parse(userIdVal);
            var user = await _authService.GetCurrentUserAsync(userId);

            return Ok(new
            {
                user.FullName,
                user.Email,
                user.PhoneNumber,
                user.Address,
                user.Role,
                user.AvatarUrl,
                user.SavedAddresses,
                user.CreatedAt
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile(UpdateProfileRequest request)
    {
        try
        {
            var userIdVal = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdVal == null) return Unauthorized();

            var userId = Guid.Parse(userIdVal);
            var success = await _authService.UpdateProfileAsync(request, userId);

            if (success)
                return Ok(new { Message = "Cập nhật thông tin thành công" });
            return BadRequest(new { Message = "Không thể cập nhật thông tin" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("users")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users.Select(u => new
            {
                u.Id,
                u.FullName,
                u.Email,
                u.PhoneNumber,
                u.Address,
                u.Role,
                u.CreatedAt
            }));
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        try
        {
            var message = await _authService.ForgotPasswordAsync(request);
            return Ok(new { Message = message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Yêu cầu thất bại: " + ex.Message });
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        try
        {
            var message = await _authService.ResetPasswordAsync(request);
            return Ok(new { Message = message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Khôi phục mật khẩu thất bại: " + ex.Message });
        }
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin(GoogleLoginRequest request)
    {
        try
        {
            var response = await _authService.GoogleLoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Đăng nhập Google thất bại: " + ex.Message });
        }
    }
}