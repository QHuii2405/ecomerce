using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Common;

namespace Domain.Entities;

public class ReturnRequest : BaseEntity
{
    [Required]
    public Guid OrderId { get; set; }
    
    [ForeignKey("OrderId")]
    public virtual Order? Order { get; set; }

    [Required]
    [MaxLength(1000)]
    public string Reason { get; set; } = string.Empty;

    // JSON string for list of image urls attached by customer
    public string? ImageUrls { get; set; }

    [Required]
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected, Refunded

    [MaxLength(1000)]
    public string? AdminNote { get; set; }
}
