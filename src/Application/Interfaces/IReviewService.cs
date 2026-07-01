using Application.DTOs;

namespace Application.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ProductReviewResponse>> GetProductReviewsAsync(Guid productId);
    Task<ProductReviewResponse> CreateReviewAsync(Guid productId, Guid userId, CreateProductReviewRequest request);
    Task<ProductReviewEligibilityResponse> GetReviewEligibilityAsync(Guid productId, Guid userId);
}
