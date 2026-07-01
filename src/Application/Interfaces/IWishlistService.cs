using Application.DTOs;

namespace Application.Interfaces;

public interface IWishlistService
{
    Task<List<ProductResponse>> GetUserWishlistAsync(Guid userId);
    Task<bool> ToggleWishlistAsync(Guid userId, Guid productId);
    Task<bool> CheckWishlistAsync(Guid userId, Guid productId);
}
