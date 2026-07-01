using Domain.Common;

namespace Domain.Entities;

public class PaymentTransaction : BaseEntity
{
    public Guid OrderId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string TransactionCode { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending";
    public decimal Amount { get; set; }
    public string PaymentUrl { get; set; } = string.Empty;
    public string QrCodeUrl { get; set; } = string.Empty;
    public string RawResponse { get; set; } = string.Empty;
    public DateTime? PaidAt { get; set; }

    public virtual Order? Order { get; set; }
}
