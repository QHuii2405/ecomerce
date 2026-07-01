namespace Application.DTOs;

public class CreatePaymentRequest
{
    public Guid OrderId { get; set; }
    public string Provider { get; set; } = "MoMo";
    public string ReturnUrl { get; set; } = string.Empty;
}

public class PaymentInitResponse
{
    public Guid OrderId { get; set; }
    public Guid TransactionId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string PaymentUrl { get; set; } = string.Empty;
    public string QrCodeUrl { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Status { get; set; } = string.Empty;
}
