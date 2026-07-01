namespace WebAPI.Controllers;

using Application.DTOs;
using Application.Interfaces;
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
}
