using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _wishlistService;

    public WishlistController(IWishlistService wishlistService)
    {
        _wishlistService = wishlistService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMyWishlist()
    {
        var userId = GetUserId();
        var items = await _wishlistService.GetUserWishlistAsync(userId);
        return Ok(new { Data = items });
    }

    [HttpPost("{productId}/toggle")]
    public async Task<IActionResult> ToggleWishlist(Guid productId)
    {
        var userId = GetUserId();
        await _wishlistService.ToggleWishlistAsync(userId, productId);
        return Ok(new { Message = "Đã cập nhật danh sách yêu thích." });
    }

    [HttpGet("{productId}/check")]
    public async Task<IActionResult> CheckWishlist(Guid productId)
    {
        var userId = GetUserId();
        var isFavorited = await _wishlistService.CheckWishlistAsync(userId, productId);
        return Ok(new { IsFavorited = isFavorited });
    }

    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}
