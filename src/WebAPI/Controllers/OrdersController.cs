namespace WebAPI.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Phải đăng nhập mới đặt hàng được
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    // ==================== CUSTOMER ENDPOINTS ====================

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var orderId = await _orderService.CreateOrderAsync(request, userId);
            return Ok(new { Message = "Đặt hàng thành công!", OrderId = orderId });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lỗi khi xử lý đơn hàng: " + ex.Message });
        }
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var orders = await _orderService.GetMyOrdersAsync(userId);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lỗi khi lấy danh sách đơn hàng: " + ex.Message });
        }
    }

    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        try
        {
            await _orderService.CancelOrderAsync(id);
            return Ok(new { Message = "Đã hủy đơn hàng và hoàn trả số lượng vào kho." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lỗi khi hủy đơn: " + ex.Message });
        }
    }

    // ==================== ADMIN / STAFF ENDPOINTS ====================

    [HttpGet]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> GetAllOrders([FromQuery] string? status = null)
    {
        try
        {
            var orders = await _orderService.GetAllOrdersAsync(status);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lỗi khi lấy danh sách đơn hàng: " + ex.Message });
        }
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderStatusRequest request)
    {
        try
        {
            await _orderService.UpdateOrderStatusAsync(id, request.NewStatus);
            return Ok(new { Message = $"Đã cập nhật trạng thái đơn hàng thành {request.NewStatus}." });
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
            return BadRequest(new { Message = "Lỗi: " + ex.Message });
        }
    }

    [HttpPost("{id}/complete")]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> CompleteOrder(Guid id)
    {
        try
        {
            await _orderService.CompleteOrderAsync(id);
            return Ok(new { Message = "Đơn hàng đã hoàn tất và trừ kho thực tế thành công!" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lỗi: " + ex.Message });
        }
    }
}