using System.Security.Claims;
using Application.DTOs.Inventory;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet("receipts")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> GetGoodsReceipts([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _inventoryService.GetGoodsReceiptsAsync();
        return Ok(result);
    }

    [HttpGet("receipts/{id}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> GetGoodsReceipt(Guid id)
    {
        var result = await _inventoryService.GetGoodsReceiptByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("receipts")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> CreateGoodsReceipt([FromBody] CreateGoodsReceiptRequest request)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _inventoryService.CreateGoodsReceiptAsync(userId, request);
        return CreatedAtAction(nameof(GetGoodsReceipt), new { id = result.Id }, result);
    }

    [HttpPut("receipts/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateGoodsReceipt(Guid id, [FromBody] UpdateGoodsReceiptRequest request)
    {
        var result = await _inventoryService.UpdateGoodsReceiptAsync(id, request);
        return Ok(result);
    }

    [HttpPost("receipts/{id}/approve")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveGoodsReceipt(Guid id)
    {
        var adminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _inventoryService.ApproveGoodsReceiptAsync(id, adminId);
        return NoContent();
    }

    [HttpPost("receipts/{id}/reject")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectGoodsReceipt(Guid id)
    {
        var adminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        await _inventoryService.RejectGoodsReceiptAsync(id, adminId);
        return NoContent();
    }

    [HttpDelete("receipts/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteGoodsReceipt(Guid id)
    {
        await _inventoryService.DeleteGoodsReceiptAsync(id);
        return NoContent();
    }
}
