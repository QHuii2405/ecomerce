using Domain.Common;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipping, Delivered, Cancelled, ReturnRequested, Refunded
    public decimal TotalAmount { get; set; }
    public decimal DiscountAmount { get; set; } = 0;
    public Guid? VoucherId { get; set; }
    public string? Note { get; set; }

    // Thông tin giao hàng
    public string? ShippingAddress { get; set; }
    public string? RecipientName { get; set; }
    public string? RecipientPhone { get; set; }
    public string? PaymentMethod { get; set; } // COD, MoMo, PayOS
    public string PaymentStatus { get; set; } = "Unpaid";

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
    public virtual ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
    public virtual Voucher? Voucher { get; set; }
}