using Domain.Common;

namespace Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Shipping, Delivered, Cancelled [cite: 92]
    public decimal TotalAmount { get; set; }
    public string? Note { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}