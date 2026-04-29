using Application.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
        {
            return BadRequest("Email này đã được sử dụng.");
        }

        // Sử dụng BCrypt để mã hóa mật khẩu trước khi lưu vào DB [cite: 54]
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            FullName = request.FullName, // Gán thêm trường này
            PhoneNumber = request.PhoneNumber, // Gán thêm trường này
            Address = request.Address, // Gán thêm trường này
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Role = "Customer"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Đăng ký thành công!");
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            return Unauthorized("Email hoặc mật khẩu không chính xác.");
        }

        // TẠO TOKEN
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("ChaoBanDayLaKeyBiMatSieuCapVipPro2026"); // Lưu ý: Nên lấy từ Config

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = "EcommerceAPI",
            Audience = "EcommerceUsers",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString, User = user.FullName });
    }
    [HttpPost("update-role")]
    // Tạm thời comment Authorize để bạn có thể tự nâng quyền cho mình mà không cần Token Admin
    // [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> UpdateRole(UpdateRoleRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
        {
            return NotFound("Không tìm thấy người dùng.");
        }

        // Kiểm tra Role hợp lệ
        var validRoles = new List<string> { "Customer", "Staff", "Admin" };
        if (!validRoles.Contains(request.NewRole))
        {
            return BadRequest("Role không hợp lệ. Chỉ chấp nhận: Customer, Staff, Admin.");
        }

        user.Role = request.NewRole;
        user.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return Ok($"Đã cập nhật quyền của {user.Email} thành {user.Role}. Hãy đăng nhập lại để lấy Token mới.");
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {
        // Lấy UserId từ Token
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == Guid.Parse(userId));

        if (user == null) return NotFound("Người dùng không tồn tại");

        return Ok(new
        {
            user.FullName,
            user.Email,
            user.PhoneNumber,
            user.Address,
            user.Role,
            user.CreatedAt
        });
    }
}