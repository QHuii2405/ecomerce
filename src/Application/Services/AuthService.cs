using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _emailService = emailService;
    }

    public async Task<string> RegisterAsync(RegisterRequest request)
    {
        var existingUsers = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        if (existingUsers.Any())
        {
            throw new InvalidOperationException("Email này đã được sử dụng.");
        }

        // Sử dụng BCrypt để mã hóa mật khẩu trước khi lưu vào DB (Mã hóa 1 lần tối ưu)
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new User
        {
            Email = request.Email,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            PasswordHash = passwordHash,
            Role = "Customer"
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        // Gửi email chào mừng
        var subject = "Chào mừng bạn đến với iLuminaty Shop!";
        var body = $"<h1>Xin chào {user.FullName},</h1><p>Cảm ơn bạn đã đăng ký tài khoản tại iLuminaty Shop. Chúc bạn có những trải nghiệm mua sắm tuyệt vời!</p>";
        // Gửi không chặn luồng chính (Fire and Forget) để phản hồi API nhanh hơn
        _ = _emailService.SendEmailAsync(user.Email, subject, body);

        return "Đăng ký thành công!";
    }

    public async Task<TokenResponse> LoginAsync(LoginRequest request)
    {
        var users = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        var user = users.FirstOrDefault();

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Email hoặc mật khẩu không chính xác.");
        }

        // Đọc cấu hình JWT từ appsettings.json
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret Key chưa được cấu hình.");
        var issuer = jwtSettings["Issuer"] ?? "EcommerceAPI";
        var audience = jwtSettings["Audience"] ?? "EcommerceUsers";
        
        // Mặc định Access Token là 15 phút, Refresh Token là 7 ngày
        var accessTokenExpiryMinutes = double.TryParse(jwtSettings["AccessTokenExpiryMinutes"], out var accessMins) ? accessMins : 15;
        var refreshTokenExpiryDays = double.TryParse(jwtSettings["RefreshTokenExpiryDays"], out var refreshDays) ? refreshDays : 7;

        // 1. Sinh Access Token (ngắn hạn)
        var accessToken = GenerateAccessToken(user, secret, issuer, audience, accessTokenExpiryMinutes);

        // 2. Sinh Refresh Token (dài hạn)
        var refreshToken = GenerateSecureToken();
        
        // 3. Cập nhật Refresh Token vào Database
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<TokenResponse> RefreshTokenAsync(TokenRequest request)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret Key chưa được cấu hình.");
        var issuer = jwtSettings["Issuer"] ?? "EcommerceAPI";
        var audience = jwtSettings["Audience"] ?? "EcommerceUsers";

        var accessTokenExpiryMinutes = double.TryParse(jwtSettings["AccessTokenExpiryMinutes"], out var accessMins) ? accessMins : 15;
        var refreshTokenExpiryDays = double.TryParse(jwtSettings["RefreshTokenExpiryDays"], out var refreshDays) ? refreshDays : 7;

        // 1. Trích xuất thông tin Claims từ Access Token đã hết hạn
        var principal = GetPrincipalFromExpiredToken(request.AccessToken, secret);
        if (principal == null)
        {
            throw new SecurityTokenException("Access Token không hợp lệ.");
        }

        var userIdVal = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdVal) || !Guid.TryParse(userIdVal, out var userId))
        {
            throw new SecurityTokenException("Access Token thiếu thông tin người dùng.");
        }

        // 2. Lấy thông tin người dùng từ Database
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new SecurityTokenException("Refresh Token không hợp lệ hoặc đã hết hạn.");
        }

        // 3. Tạo mới Access Token và Refresh Token (Cơ chế Token Rotation bảo mật)
        var newAccessToken = GenerateAccessToken(user, secret, issuer, audience, accessTokenExpiryMinutes);
        var newRefreshToken = GenerateSecureToken();

        // 4. Lưu lại thông tin Refresh Token mới vào Database
        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new TokenResponse
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }

    public async Task<bool> RevokeTokenAsync(string email)
    {
        var users = await _unitOfWork.Users.FindAsync(u => u.Email == email);
        var user = users.FirstOrDefault();
        if (user == null) return false;

        user.RefreshToken = null;
        user.RefreshTokenExpiryTime = null;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<string> UpdateRoleAsync(UpdateRoleRequest request)
    {
        var users = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        var user = users.FirstOrDefault();

        if (user == null)
        {
            throw new KeyNotFoundException("Không tìm thấy người dùng.");
        }

        // Kiểm tra Role hợp lệ
        var validRoles = new List<string> { "Customer", "Staff", "Admin" };
        if (!validRoles.Contains(request.NewRole))
        {
            throw new ArgumentException("Role không hợp lệ. Chỉ chấp nhận: Customer, Staff, Admin.");
        }

        user.Role = request.NewRole;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return $"Đã cập nhật quyền của {user.Email} thành {user.Role}. Hãy đăng nhập lại để lấy Token mới.";
    }

    public async Task<User> GetCurrentUserAsync(Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("Người dùng không tồn tại");
        }

        return user;
    }

    public async Task<bool> UpdateProfileAsync(UpdateProfileRequest request, Guid userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            throw new KeyNotFoundException("Người dùng không tồn tại");

        if (request.FullName != null) user.FullName = request.FullName;
        if (request.PhoneNumber != null) user.PhoneNumber = request.PhoneNumber;
        if (request.AvatarUrl != null) user.AvatarUrl = request.AvatarUrl;
        if (request.SavedAddresses != null) user.SavedAddresses = request.SavedAddresses;

        user.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _unitOfWork.Users.GetAllAsync();
    }

    public async Task<string> ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var users = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        var user = users.FirstOrDefault();
        if (user == null)
        {
            throw new KeyNotFoundException("Email không tồn tại trong hệ thống.");
        }

        var otp = new Random().Next(100000, 999999).ToString();
        user.OtpCode = otp;
        user.OtpExpiryTime = DateTime.UtcNow.AddMinutes(15);
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        var subject = "Mã xác nhận khôi phục mật khẩu";
        var body = $"<h2>Mã OTP của bạn là: {otp}</h2><p>Mã này sẽ hết hạn trong 15 phút. Vui lòng không chia sẻ mã này với bất kỳ ai.</p>";
        await _emailService.SendEmailAsync(user.Email, subject, body);

        return "Mã OTP đã được gửi đến email của bạn.";
    }

    public async Task<string> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var users = await _unitOfWork.Users.FindAsync(u => u.Email == request.Email);
        var user = users.FirstOrDefault();
        if (user == null)
        {
            throw new KeyNotFoundException("Email không tồn tại trong hệ thống.");
        }

        if (user.OtpCode != request.OtpCode)
        {
            throw new ArgumentException("Mã OTP không chính xác.");
        }

        if (user.OtpExpiryTime < DateTime.UtcNow)
        {
            throw new ArgumentException("Mã OTP đã hết hạn.");
        }

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        user.OtpCode = null;
        user.OtpExpiryTime = null;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return "Mật khẩu đã được đặt lại thành công.";
    }

    public async Task<TokenResponse> GoogleLoginAsync(GoogleLoginRequest request)
    {
        using var httpClient = new HttpClient();
        // useGoogleLogin returns access_token in implicit flow. 
        // We can verify it or get user info via userinfo endpoint.
        var response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v3/userinfo?access_token={request.IdToken}");
        if (!response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException("Token Google không hợp lệ.");
        }

        var json = await response.Content.ReadAsStringAsync();
        using var document = JsonDocument.Parse(json);
        var root = document.RootElement;

        if (!root.TryGetProperty("email", out var emailProp))
        {
            throw new UnauthorizedAccessException("Không thể lấy email từ Google.");
        }
        var email = emailProp.GetString()!;
        var name = root.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "Google User";
        var picture = root.TryGetProperty("picture", out var picProp) ? picProp.GetString() : null;

        var users = await _unitOfWork.Users.FindAsync(u => u.Email == email);
        var user = users.FirstOrDefault();

        if (user == null)
        {
            user = new User
            {
                Email = email,
                FullName = name ?? "Google User",
                AvatarUrl = picture,
                Role = "Customer",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Guid.NewGuid().ToString())
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secret = jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret Key chưa được cấu hình.");
        var issuer = jwtSettings["Issuer"] ?? "EcommerceAPI";
        var audience = jwtSettings["Audience"] ?? "EcommerceUsers";
        var accessTokenExpiryMinutes = double.TryParse(jwtSettings["AccessTokenExpiryMinutes"], out var accessMins) ? accessMins : 15;
        var refreshTokenExpiryDays = double.TryParse(jwtSettings["RefreshTokenExpiryDays"], out var refreshDays) ? refreshDays : 7;

        var accessToken = GenerateAccessToken(user, secret, issuer, audience, accessTokenExpiryMinutes);
        var refreshToken = GenerateSecureToken();
        
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(refreshTokenExpiryDays);
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }

    // --- CÁC PHƯƠNG THỨC TRỢ GIÚP (HELPERS) ---

    private string GenerateAccessToken(User user, string secret, string issuer, string audience, double expiryMinutes)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(expiryMinutes),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private string GenerateSecureToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string token, string secret)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
            ValidateLifetime = false // Bắt buộc bỏ qua thời gian hết hạn để giải mã token cũ
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        
        if (securityToken is not JwtSecurityToken jwtSecurityToken || 
            !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Token không hợp lệ.");
        }

        return principal;
    }
}
