namespace WebAPI.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IOrderService _orderService;

    public PaymentsController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Xử lý thanh toán COD (Cash on Delivery)
    /// </summary>
    [HttpPost("cod")]
    public async Task<IActionResult> PayByCod([FromBody] CodPaymentRequest request)
    {
        try
        {
            // COD: đơn hàng giữ trạng thái Pending, Staff sẽ confirm khi giao
            return Ok(new
            {
                Message = "Đơn hàng COD đã được ghi nhận. Bạn sẽ thanh toán khi nhận hàng.",
                OrderId = request.OrderId,
                PaymentMethod = "COD",
                Status = "Pending"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Tạo QR thanh toán MoMo (giả lập)
    /// </summary>
    [HttpPost("momo/create")]
    public async Task<IActionResult> CreateMomoQr([FromBody] CreateQrPaymentRequest request)
    {
        try
        {
            // Tạo mã QR giả lập — trong production sẽ gọi MoMo API thật
            var qrToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).TrimEnd('=');
            var deeplink = $"momo://payment?orderId={request.OrderId}&amount={request.Amount}&token={qrToken}";

            return Ok(new
            {
                OrderId = request.OrderId,
                Amount = request.Amount,
                QrToken = qrToken,
                DeepLink = deeplink,
                // QR image sử dụng QR generator public API
                QrImageUrl = $"https://api.qrserver.com/v1/create-qr-code/?size=250x250&data={Uri.EscapeDataString(deeplink)}",
                ExpiresInSeconds = 300, // 5 phút
                PaymentMethod = "MoMo"
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Giả lập callback từ MoMo sau khi thanh toán thành công
    /// </summary>
    [HttpPost("momo/confirm")]
    public async Task<IActionResult> ConfirmMomoPayment([FromBody] ProcessPaymentRequest request)
    {
        try
        {
            var success = await _orderService.ProcessPaymentAsync(request.OrderId, request.SimulateSuccess);
            if (success)
            {
                return Ok(new
                {
                    Message = "Thanh toán MoMo thành công! Đơn hàng đã được xác nhận.",
                    PaymentMethod = "MoMo",
                    Status = "Confirmed"
                });
            }
            return BadRequest(new { Message = "Thanh toán thất bại. Vui lòng thử lại." });
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
            return BadRequest(new { Message = "Lỗi xử lý thanh toán: " + ex.Message });
        }
    }

    /// <summary>
    /// Tạo VietQR (Bank Transfer QR)
    /// </summary>
    [HttpPost("vietqr/create")]
    public async Task<IActionResult> CreateVietQr([FromBody] CreateQrPaymentRequest request)
    {
        try
        {
            // VietQR chuẩn Napas — bank: MB Bank, account tĩnh cho demo
            const string bankId = "MB";
            const string accountNo = "0123456789";
            const string accountName = "LUMINA TECH STORE";
            var transferContent = $"LUMINA{request.OrderId.ToString().Replace("-", "")[..8].ToUpper()}";

            var qrData = $"https://img.vietqr.io/image/{bankId}-{accountNo}-compact.png?amount={request.Amount}&addInfo={Uri.EscapeDataString(transferContent)}&accountName={Uri.EscapeDataString(accountName)}";

            return Ok(new
            {
                OrderId = request.OrderId,
                Amount = request.Amount,
                BankId = bankId,
                AccountNo = accountNo,
                AccountName = accountName,
                TransferContent = transferContent,
                QrImageUrl = qrData,
                PaymentMethod = "VietQR",
                Note = "Vui lòng chuyển khoản đúng nội dung để đơn hàng được xử lý tự động."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    /// <summary>
    /// Legacy: xử lý thanh toán đơn giản (giữ tương thích)
    /// </summary>
    [HttpPost("process")]
    public async Task<IActionResult> ProcessPayment(ProcessPaymentRequest request)
    {
        try
        {
            var success = await _orderService.ProcessPaymentAsync(request.OrderId, request.SimulateSuccess);
            if (success)
            {
                return Ok(new { Message = "Thanh toán thành công. Đơn hàng đã được xác nhận!" });
            }
            return BadRequest(new { Message = "Thanh toán thất bại. Vui lòng thử lại." });
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
            return BadRequest(new { Message = "Lỗi xử lý thanh toán: " + ex.Message });
        }
    }
}