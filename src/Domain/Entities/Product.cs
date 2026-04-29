using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    
    // Liên kết với Inventory [cite: 94, 109]
    public virtual Inventory? Inventory { get; set; }
}