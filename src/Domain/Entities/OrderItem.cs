using Domain.Common;

namespace Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } // Lưu giá tại thời điểm mua
    public string VariantSnapshotJson { get; set; } = "{}";

    // Navigation properties
    public virtual Product? Product { get; set; }
    public virtual ProductVariant? ProductVariant { get; set; }
    public virtual Order? Order { get; set; }
}