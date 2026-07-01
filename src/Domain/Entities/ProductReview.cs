using Domain.Common;

namespace Domain.Entities;

public class ProductReview : BaseEntity
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string? ImageUrls { get; set; } // JSON array of strings

    public virtual Product? Product { get; set; }
    public virtual User? User { get; set; }
    public virtual Order? Order { get; set; }
}
