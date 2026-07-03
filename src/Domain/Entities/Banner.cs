using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Common;

namespace Domain.Entities;

public class Banner : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Subtitle { get; set; }

    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? TargetUrl { get; set; }

    public bool IsActive { get; set; }

    public int DisplayOrder { get; set; }
}
