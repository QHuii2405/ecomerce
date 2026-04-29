using Application.DTOs;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PaymentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment(ProcessPaymentRequest request)
    {
        var order = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == request.OrderId);

        if (order == null) return NotFound("Không tìm thấy đơn hàng.");
        if (order.Status != "Pending") return BadRequest("Đơn hàng không ở trạng thái chờ thanh toán.");

        if (request.SimulateSuccess)
        {
            // Thanh toán thành công 
            order.Status = "Confirmed";
            order.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return Ok(new { Message = "Thanh toán thành công. Đơn hàng đã được xác nhận!" });
        }
        else
        {
            // Thanh toán thất bại [cite: 78]
            return BadRequest("Thanh toán thất bại. Vui lòng thử lại.");
        }
    }
}