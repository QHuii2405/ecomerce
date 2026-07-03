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
        var reviews = await _unitOfWork.Repository<ProductReview>().FindAsync(r => r.ProductId == productId && !r.IsDeleted, includeProperties: "Product");
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
        var product = await _unitOfWork.Repository<Product>().GetByIdAsync(productId);
        review.Product = product;
        return MapToResponse(review, user?.FullName ?? "Khách hàng");
    }

    private static ProductReviewResponse MapToResponse(ProductReview review, string userName)
    {
        return new ProductReviewResponse
        {
            Id = review.Id,
            ProductId = review.ProductId,
            ProductName = review.Product?.Name ?? "Sản phẩm không xác định",
            UserId = review.UserId,
            OrderId = review.OrderId,
            UserName = userName,
            Rating = review.Rating,
            Comment = review.Comment,
            AdminReply = review.AdminReply,
            CreatedAt = review.CreatedAt
        };
    }

    public async Task<ProductReviewEligibilityResponse> GetReviewEligibilityAsync(Guid productId, Guid userId)
    {
        var orders = await _unitOfWork.Orders.GetOrdersByUserIdWithItemsAsync(userId);
        var deliveredOrdersWithProduct = orders.Where(o => o.Status == "Delivered" && o.OrderItems.Any(i => i.ProductId == productId));

        if (!deliveredOrdersWithProduct.Any())
        {
            return new ProductReviewEligibilityResponse { CanReview = false, Reason = "Bạn cần mua và nhận hàng thành công trước khi đánh giá." };
        }

        var hasReviewed = await _unitOfWork.Repository<ProductReview>().FindAsync(r => 
            r.ProductId == productId && r.UserId == userId && !r.IsDeleted);

        if (hasReviewed.Any())
        {
            return new ProductReviewEligibilityResponse { CanReview = false, Reason = "Bạn đã đánh giá sản phẩm này." };
        }

        var eligibleOrder = deliveredOrdersWithProduct.OrderByDescending(o => o.CreatedAt).First();
        return new ProductReviewEligibilityResponse { CanReview = true, EligibleOrderId = eligibleOrder.Id };
    }

    public async Task<IEnumerable<ProductReviewResponse>> GetAllReviewsAdminAsync()
    {
        var reviews = await _unitOfWork.Repository<ProductReview>().FindAsync(r => !r.IsDeleted, includeProperties: "Product");
        var users = await _unitOfWork.Users.GetAllAsync();
        var userLookup = users.ToDictionary(u => u.Id, u => u.FullName);

        return reviews
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => MapToResponse(r, userLookup.GetValueOrDefault(r.UserId, "Khách hàng")));
    }

    public async Task<bool> ReplyToReviewAsync(Guid reviewId, string reply)
    {
        var review = await _unitOfWork.Repository<ProductReview>().GetByIdAsync(reviewId);
        if (review == null || review.IsDeleted)
        {
            throw new KeyNotFoundException("Đánh giá không tồn tại hoặc đã bị xóa.");
        }

        review.AdminReply = string.IsNullOrWhiteSpace(reply) ? null : reply.Trim();
        review.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Repository<ProductReview>().Update(review);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteReviewAsync(Guid reviewId)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var review = await _unitOfWork.Repository<ProductReview>().GetByIdAsync(reviewId);
            if (review == null || review.IsDeleted)
            {
                throw new KeyNotFoundException("Đánh giá không tồn tại hoặc đã bị xóa.");
            }

            review.IsDeleted = true;
            review.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Repository<ProductReview>().Update(review);
            await _unitOfWork.CommitTransactionAsync();
            
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
