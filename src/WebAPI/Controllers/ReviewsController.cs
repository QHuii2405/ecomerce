namespace WebAPI.Controllers;

using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/products/{productId:guid}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewsController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(Guid productId)
    {
        var reviews = await _reviewService.GetProductReviewsAsync(productId);
        return Ok(reviews);
    }

    [HttpGet("eligibility")]
    [Authorize]
    public async Task<IActionResult> GetReviewEligibility(Guid productId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var eligibility = await _reviewService.GetReviewEligibilityAsync(productId, userId);
        return Ok(eligibility);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReview(Guid productId, CreateProductReviewRequest request)
    {
        try
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var review = await _reviewService.CreateReviewAsync(productId, userId, request);
            return Ok(review);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpGet("/api/admin/reviews")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> GetAllReviewsAdmin()
    {
        var reviews = await _reviewService.GetAllReviewsAdminAsync();
        return Ok(reviews);
    }

    [HttpDelete("/api/admin/reviews/{reviewId:guid}")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> DeleteReviewAdmin(Guid reviewId)
    {
        try
        {
            await _reviewService.DeleteReviewAsync(reviewId);
            return Ok(new { Message = "Đã xóa đánh giá thành công." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("/api/admin/reviews/{reviewId:guid}/reply")]
    [Authorize(Roles = "Admin,Staff")]
    public async Task<IActionResult> ReplyToReviewAdmin(Guid reviewId, [FromBody] ReviewReplyRequest request)
    {
        try
        {
            await _reviewService.ReplyToReviewAsync(reviewId, request.Reply);
            return Ok(new { Message = "Đã trả lời đánh giá thành công." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}

public class ReviewReplyRequest
{
    public string Reply { get; set; } = string.Empty;
}
