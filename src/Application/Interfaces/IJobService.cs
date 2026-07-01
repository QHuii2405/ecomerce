using Domain.Entities;

namespace Application.Interfaces;

public interface IJobService
{
    Task<IEnumerable<JobPosting>> GetActiveJobsAsync();
    Task<JobPosting?> GetJobByIdAsync(Guid id);
}
