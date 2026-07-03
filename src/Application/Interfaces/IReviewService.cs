using Application.DTOs;

namespace Application.Interfaces;

public interface IReviewService
{
    Task<IEnumerable<ProductReviewResponse>> GetProductReviewsAsync(Guid productId);
    Task<ProductReviewResponse> CreateReviewAsync(Guid productId, Guid userId, CreateProductReviewRequest request);
    Task<ProductReviewEligibilityResponse> GetReviewEligibilityAsync(Guid productId, Guid userId);
    Task<IEnumerable<ProductReviewResponse>> GetAllReviewsAdminAsync();
    Task<bool> ReplyToReviewAsync(Guid reviewId, string reply);
    Task<bool> DeleteReviewAsync(Guid reviewId);
}
