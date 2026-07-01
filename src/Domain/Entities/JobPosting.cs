using Domain.Common;

namespace Domain.Entities;

public class JobPosting : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = string.Empty;
    public string SalaryRange { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public string Benefits { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public DateTime PostedAt { get; set; } = DateTime.UtcNow;
}
