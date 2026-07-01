using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;

    public ReviewService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ProductReviewResponse>> GetProductReviewsAsync(Guid productId)
    {
        var reviews = await _unitOfWork.Repository<ProductReview>().FindAsync(r => r.ProductId == productId && !r.IsDeleted);
        var users = await _unitOfWork.Users.GetAllAsync();
        var userLookup = users.ToDictionary(u => u.Id, u => u.FullName);

        return reviews
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => MapToResponse(r, userLookup.GetValueOrDefault(r.UserId, "Khách hàng")));
    }

    public async Task<ProductReviewResponse> CreateReviewAsync(Guid productId, Guid userId, CreateProductReviewRequest request)
    {
        if (request.Rating < 1 || request.Rating > 5)
        {
            throw new ArgumentException("Số sao đánh giá phải từ 1 đến 5.");
        }

        var existingReviews = await _unitOfWork.Repository<ProductReview>().FindAsync(r =>
            r.ProductId == productId && r.UserId == userId && !r.IsDeleted);
        if (existingReviews.Any())
        {
            throw new InvalidOperationException("Bạn đã đánh giá sản phẩm này rồi.");
        }

        var order = await _unitOfWork.Orders.GetOrderWithItemsAsync(request.OrderId);
        if (order == null || order.UserId != userId || order.Status != "Delivered")
        {
            throw new InvalidOperationException("Chỉ người đã mua và nhận hàng thành công mới được đánh giá.");
        }

        var purchasedProduct = order.OrderItems.Any(i => i.ProductId == productId);
        if (!purchasedProduct)
        {
            throw new InvalidOperationException("Đơn hàng này không chứa sản phẩm cần đánh giá.");
        }

        var review = new ProductReview
        {
            ProductId = productId,
            UserId = userId,
            OrderId = request.OrderId,
            Rating = request.Rating,
            Comment = request.Comment.Trim()
        };

        await _unitOfWork.Repository<ProductReview>().AddAsync(review);
        await _unitOfWork.SaveChangesAsync();

        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        return MapToResponse(review, user?.FullName ?? "Khách hàng");
    }

    private static ProductReviewResponse MapToResponse(ProductReview review, string userName)
    {
        return new ProductReviewResponse
        {
            Id = review.Id,
            ProductId = review.ProductId,
            UserId = review.UserId,
            OrderId = review.OrderId,
            UserName = userName,
            Rating = review.Rating,
            Comment = review.Comment,
            CreatedAt = review.CreatedAt
        };
    }
}
