using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CmsController : ControllerBase
{
    private readonly ICmsService _cmsService;

    public CmsController(ICmsService cmsService)
    {
        _cmsService = cmsService;
    }

    // ==================== BANNERS ====================

    [HttpGet("banners")]
    public async Task<IActionResult> GetBanners([FromQuery] bool onlyActive = true)
    {
        var banners = await _cmsService.GetBannersAsync(onlyActive);
        return Ok(new { data = banners });
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpPost("banners")]
    public async Task<IActionResult> CreateBanner([FromBody] CreateBannerRequest request)
    {
        var banner = await _cmsService.CreateBannerAsync(request);
        return Ok(new { data = banner });
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpPut("banners/{id}")]
    public async Task<IActionResult> UpdateBanner(Guid id, [FromBody] CreateBannerRequest request)
    {
        var banner = await _cmsService.UpdateBannerAsync(id, request);
        return Ok(new { data = banner });
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpDelete("banners/{id}")]
    public async Task<IActionResult> DeleteBanner(Guid id)
    {
        await _cmsService.DeleteBannerAsync(id);
        return Ok(new { message = "Deleted successfully" });
    }

    // ==================== BLOG POSTS ====================

    [HttpGet("blogs")]
    public async Task<IActionResult> GetBlogs([FromQuery] bool onlyPublished = true)
    {
        var blogs = await _cmsService.GetBlogPostsAsync(onlyPublished);
        return Ok(new { data = blogs });
    }

    [HttpGet("blogs/{slug}")]
    public async Task<IActionResult> GetBlogBySlug(string slug)
    {
        var blog = await _cmsService.GetBlogPostBySlugAsync(slug);
        return Ok(new { data = blog });
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpGet("blogs/id/{id}")]
    public async Task<IActionResult> GetBlogById(Guid id)
    {
        var blog = await _cmsService.GetBlogPostByIdAsync(id);
        return Ok(new { data = blog });
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpPost("blogs")]
    public async Task<IActionResult> CreateBlog([FromBody] CreateBlogPostRequest request)
    {
        var authorId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var blog = await _cmsService.CreateBlogPostAsync(request, authorId);
        return Ok(new { data = blog });
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpPut("blogs/{id}")]
    public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] CreateBlogPostRequest request)
    {
        var blog = await _cmsService.UpdateBlogPostAsync(id, request);
        return Ok(new { data = blog });
    }

    [Authorize(Roles = "Admin,Staff")]
    [HttpDelete("blogs/{id}")]
    public async Task<IActionResult> DeleteBlog(Guid id)
    {
        await _cmsService.DeleteBlogPostAsync(id);
        return Ok(new { message = "Deleted successfully" });
    }
}
