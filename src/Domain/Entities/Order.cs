using Domain.Common;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipping, Delivered, Cancelled
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }

    // Thông tin giao hàng
    public string? ShippingAddress { get; set; }
    public string? RecipientName { get; set; }
    public string? RecipientPhone { get; set; }
    public string? PaymentMethod { get; set; } // COD, MoMo, VietQR

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}