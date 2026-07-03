using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Domain.Common;

namespace Domain.Entities;

public class BlogPost : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(250)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    public string Content { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? ThumbnailUrl { get; set; }

    [Required]
    public Guid AuthorId { get; set; }
    
    [ForeignKey("AuthorId")]
    public virtual User? Author { get; set; }

    public bool IsPublished { get; set; } = false;
}
