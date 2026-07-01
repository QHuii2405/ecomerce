using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var jobs = await _jobService.GetActiveJobsAsync();
        return Ok(new { Data = jobs });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var job = await _jobService.GetJobByIdAsync(id);
        if (job == null)
        {
            return NotFound(new { Message = "Khong tim thay tin tuyen dung." });
        }

        return Ok(job);
    }
}
