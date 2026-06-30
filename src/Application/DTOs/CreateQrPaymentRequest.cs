namespace Application.DTOs;

public class CreateQrPaymentRequest
{
    public Guid OrderId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = "MoMo"; // MoMo | VietQR
}

public class CodPaymentRequest
{
    public Guid OrderId { get; set; }
}
