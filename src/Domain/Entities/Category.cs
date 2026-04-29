using Domain.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Hỗ trợ danh mục lồng nhau (Parent-Child) [cite: 109]
    public Guid? ParentId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}