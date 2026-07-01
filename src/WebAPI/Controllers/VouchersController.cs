using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VouchersController : ControllerBase
{
    private readonly IVoucherService _voucherService;

    public VouchersController(IVoucherService voucherService)
    {
        _voucherService = voucherService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> GetAll()
    {
        var vouchers = await _voucherService.GetAllVouchersAsync();
        return Ok(new { Data = vouchers });
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> Create(CreateVoucherRequest request)
    {
        try
        {
            var voucher = await _voucherService.CreateVoucherAsync(request);
            return Ok(new { Message = "Tạo mã giảm giá thành công", Data = voucher });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("apply")]
    [Authorize]
    public async Task<IActionResult> Apply(ApplyVoucherRequest request)
    {
        try
        {
            var result = await _voucherService.ValidateAndApplyVoucherAsync(request);
            return Ok(new { Message = "Áp dụng mã giảm giá thành công", Data = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("{id}/toggle")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        try
        {
            await _voucherService.ToggleVoucherStatusAsync(id);
            return Ok(new { Message = "Cập nhật trạng thái thành công." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}
