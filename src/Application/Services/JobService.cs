using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class JobService : IJobService
{
    private readonly IUnitOfWork _unitOfWork;

    public JobService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<JobPosting>> GetActiveJobsAsync()
    {
        var jobs = await _unitOfWork.Repository<JobPosting>().GetAllAsync();
        return jobs
            .Where(j => !j.IsDeleted && j.IsActive)
            .OrderByDescending(j => j.PostedAt);
    }

    public async Task<JobPosting?> GetJobByIdAsync(Guid id)
    {
        var job = await _unitOfWork.Repository<JobPosting>().GetByIdAsync(id);
        if (job == null || job.IsDeleted || !job.IsActive)
        {
            return null;
        }

        return job;
    }
}
