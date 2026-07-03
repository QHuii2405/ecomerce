using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReturnController : ControllerBase
{
    private readonly IOrderService _orderService;

    public ReturnController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // Khách hàng yêu cầu hoàn trả
    [HttpPost("request")]
    public async Task<IActionResult> RequestReturn([FromBody] CreateReturnRequest request)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

        var userId = Guid.Parse(userIdStr);
        try
        {
            var result = await _orderService.RequestReturnAsync(userId, request);
            return Ok(new { data = result, message = "Đã gửi yêu cầu hoàn trả thành công." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    // Admin lấy danh sách
    [Authorize(Roles = "Admin,Staff")]
    [HttpGet]
    public async Task<IActionResult> GetReturnRequests()
    {
        var requests = await _orderService.GetReturnRequestsAsync();
        return Ok(new { data = requests });
    }

    // Admin duyệt/từ chối
    [Authorize(Roles = "Admin,Staff")]
    [HttpPost("{id}/process")]
    public async Task<IActionResult> ProcessReturnRequest(Guid id, [FromBody] ProcessReturnRequest request)
    {
        try
        {
            var success = await _orderService.ProcessReturnRequestAsync(id, request);
            return Ok(new { success, message = request.Approve ? "Đã duyệt hoàn trả thành công." : "Đã từ chối hoàn trả." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
