using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public bool IsNew { get; set; } = false;
    public Guid CategoryId { get; set; }
    public string Brand { get; set; } = "iLuminaty";
    public string AttributesJson { get; set; } = "{}";
    public List<string> ImageUrls { get; set; } = new();
    
    public virtual Category? Category { get; set; }
    public virtual Inventory? Inventory { get; set; }
    public virtual ICollection<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
    public virtual ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
}