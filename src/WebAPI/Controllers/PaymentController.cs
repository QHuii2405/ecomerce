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
    private readonly IPaymentGatewayService _paymentGatewayService;
    private readonly IMomoPaymentProvider _momoProvider;
    private readonly IPayOsPaymentProvider _payOsProvider;

    public PaymentsController(IOrderService orderService, IPaymentGatewayService paymentGatewayService, IMomoPaymentProvider momoProvider, IPayOsPaymentProvider payOsProvider)
    {
        _orderService = orderService;
        _paymentGatewayService = paymentGatewayService;
        _momoProvider = momoProvider;
        _payOsProvider = payOsProvider;
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

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
    {
        try
        {
            var response = await _paymentGatewayService.CreatePaymentAsync(request);
            return Ok(response);
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
            return BadRequest(new { Message = "Lỗi tạo giao dịch thanh toán: " + ex.Message });
        }
    }

    [HttpGet("{orderId}/status")]
    public async Task<IActionResult> GetPaymentStatus(Guid orderId)
    {
        var order = await _orderService.GetOrderByIdAsync(orderId);
        if (order == null) return NotFound("Không tìm thấy đơn hàng.");
        
        return Ok(new {
            OrderId = order.Id,
            Status = order.Status,
            PaymentStatus = order.PaymentStatus
        });
    }

    [AllowAnonymous]
    [HttpPost("momo/webhook")]
    public async Task<IActionResult> MomoWebhook([FromBody] MomoWebhookRequest request)
    {
        try
        {
            var isValid = _momoProvider.VerifySignature(
                request.partnerCode, request.orderId, request.requestId, request.amount,
                request.orderInfo, request.orderType, request.transId, request.resultCode,
                request.message, request.payType, request.responseTime, request.extraData, request.signature);

            if (!isValid) return BadRequest(new { Message = "Invalid signature" });

            if (request.resultCode == "0") // Success
            {
                // MoMo returns our transactionCode in the orderId field
                await _paymentGatewayService.ConfirmPaymentByTransactionCodeAsync("MoMo", request.orderId);
            }

            return Ok(new { Message = "Webhook processed" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [AllowAnonymous]
    [HttpPost("payos/webhook")]
    public async Task<IActionResult> PayOsWebhook([FromBody] System.Text.Json.JsonElement requestBody, [FromHeader(Name = "x-payos-signature")] string signature)
    {
        try
        {
            // Note: In a real app, verify PayOS signature here using checksum key
            // The requestBody has a "data" object containing "orderCode" and "code"
            if (requestBody.TryGetProperty("data", out var dataObj) && dataObj.ValueKind != System.Text.Json.JsonValueKind.Null)
            {
                if (dataObj.TryGetProperty("orderCode", out var orderCodeElement))
                {
                    var orderCode = orderCodeElement.GetInt64().ToString();
                    
                    if (requestBody.TryGetProperty("code", out var codeElement) && codeElement.GetString() == "00")
                    {
                        await _paymentGatewayService.ConfirmPaymentByTransactionCodeAsync("PayOS", orderCode);
                    }
                }
            }

            return Ok(new { Message = "Webhook processed" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}

public class MomoWebhookRequest
{
    public string partnerCode { get; set; }
    public string orderId { get; set; }
    public string requestId { get; set; }
    public string amount { get; set; }
    public string orderInfo { get; set; }
    public string orderType { get; set; }
    public string transId { get; set; }
    public string resultCode { get; set; }
    public string message { get; set; }
    public string payType { get; set; }
    public string responseTime { get; set; }
    public string extraData { get; set; }
    public string signature { get; set; }
}