using Microsoft.AspNetCore.Mvc;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TestController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("check-database")]
    public async Task<IActionResult> CheckDatabase()
    {
        // Kiểm tra xem có kết nối được tới DB trong Docker không
        var canConnect = await _context.Database.CanConnectAsync();

        if (canConnect)
            return Ok("Kết nối Database thành công! Dự án của bạn đã sẵn sàng.");

        return BadRequest("Không thể kết nối tới Database. Hãy kiểm tra lại Docker.");
    }
    [Authorize] // Chỉ ai có Token mới gọi được hàm này
    [HttpGet("profile")]
    public IActionResult GetProfile()
    {
        // Lấy thông tin từ Token đã gửi lên
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
        var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

        return Ok(new
        {
            Message = "Chào mừng bạn, đây là thông tin lấy từ Token!",
            Email = email,
            Role = role
        });
    }
}