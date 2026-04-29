using Application.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Phải đăng nhập mới đặt hàng được [cite: 11]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var order = new Order
            {
                UserId = userId,
                Status = "Pending",
                TotalAmount = 0
            };

            foreach (var item in request.Items)
            {

                // 1. Kiểm tra sản phẩm và kho + Giữ chỗ kho bằng atomic SQL
                var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
                    @"UPDATE Inventories 
                      SET ReservedQuantity = ReservedQuantity + {1}
                      WHERE ProductId = {0} 
                      AND (StockQuantity - ReservedQuantity) >= {1}",
                    item.ProductId, item.Quantity);

                if (rowsAffected == 0)
                {
                    return BadRequest($"Sản phẩm {item.ProductId} không đủ hàng hoặc đã bị đặt bởi người khác!");
                }

                // 2. Lấy giá sản phẩm hiện tại
                var product = await _context.Products.FindAsync(item.ProductId);

                // 4. Thêm vào chi tiết đơn hàng
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = product!.Price
                };
                order.OrderItems.Add(orderItem);
                order.TotalAmount += orderItem.UnitPrice * orderItem.Quantity;
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new { Message = "Đặt hàng thành công!", OrderId = order.Id });
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return BadRequest("Lỗi khi xử lý đơn hàng: " + ex.Message);
        }
    }
    [HttpPost("{id}/complete")]

    [Authorize(Roles = "Staff,Admin")] // Chỉ nhân viên mới được xác nhận giao xong [cite: 28, 82]
    public async Task<IActionResult> CompleteOrder(Guid id)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound("Đơn hàng không tồn tại.");
        if (order.Status != "Confirmed") return BadRequest("Chỉ có thể hoàn tất các đơn đã xác nhận.");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var item in order.OrderItems)
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(i => i.ProductId == item.ProductId);

                if (inventory != null)
                {
                    // TRỪ KHO THẬT: Giảm cả Stock và Reserved 
                    inventory.StockQuantity -= item.Quantity;
                    inventory.ReservedQuantity -= item.Quantity;
                }
            }

            order.Status = "Delivered"; // Giao hàng thành công 
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok("Đơn hàng đã hoàn tất và trừ kho thực tế thành công!");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return BadRequest("Lỗi: " + ex.Message);
        }
    }
    // 1. API cho khách xem lịch sử đơn hàng
    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var orders = await _context.Orders
            .Include(o => o.OrderItems)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return Ok(orders);
    }

    // 2. API Hủy đơn hàng và Hoàn trả kho (Stock Return)
    [HttpPost("{id}/cancel")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound("Đơn hàng không tồn tại.");

        // Chỉ được hủy khi đơn đang ở trạng thái Pending hoặc Confirmed
        if (order.Status != "Pending" && order.Status != "Confirmed")
            return BadRequest("Không thể hủy đơn hàng ở trạng thái này.");

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            foreach (var item in order.OrderItems)
            {
                var inventory = await _context.Inventories
                    .FirstOrDefaultAsync(i => i.ProductId == item.ProductId);
                if (inventory != null)
                {
                    // Trả lại số lượng giữ chỗ về Available
                    inventory.ReservedQuantity -= item.Quantity;
                }
            }

            order.Status = "Cancelled";
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok("Đã hủy đơn hàng và hoàn trả số lượng vào kho.");
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return BadRequest("Lỗi khi hủy đơn: " + ex.Message);
        }
    }
}