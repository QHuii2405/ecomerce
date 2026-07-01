using Domain.Common;

namespace Domain.Entities;

public class ProductVariant : BaseEntity
{
    public Guid ProductId { get; set; }
    public string Sku { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string AttributesJson { get; set; } = "{}";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int ReservedQuantity { get; set; }
    public bool IsActive { get; set; } = true;

    public int AvailableQuantity => StockQuantity - ReservedQuantity;

    public virtual Product? Product { get; set; }
}
