using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System.Text.Json;

namespace Application.Services;
public class WishlistService : IWishlistService
{
    private readonly IUnitOfWork _unitOfWork;

    public WishlistService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<ProductResponse>> GetUserWishlistAsync(Guid userId)
    {
        var items = await _unitOfWork.Repository<WishlistItem>().FindAsync(w => w.UserId == userId && !w.IsDeleted);
        var productIds = items.Select(w => w.ProductId).ToList();
        
        var products = await _unitOfWork.Products.FindAsync(p => productIds.Contains(p.Id) && !p.IsDeleted);
        
        // Map to DTOs and attach images and variants
        var result = new List<ProductResponse>();
        foreach (var p in products)
        {
            var dto = new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                CategoryId = p.CategoryId,
                Brand = p.Brand,
                Attributes = string.IsNullOrEmpty(p.AttributesJson) ? new Dictionary<string, string>() : JsonSerializer.Deserialize<Dictionary<string, string>>(p.AttributesJson) ?? new Dictionary<string, string>()
            };

            var variants = await _unitOfWork.Repository<ProductVariant>().FindAsync(v => v.ProductId == p.Id && !v.IsDeleted);
            dto.Variants = variants.Select(v => new ProductVariantResponse
            {
                Id = v.Id,
                ProductId = v.ProductId,
                Sku = v.Sku,
                Name = v.Name,
                Attributes = string.IsNullOrEmpty(v.AttributesJson) ? new Dictionary<string, string>() : JsonSerializer.Deserialize<Dictionary<string, string>>(v.AttributesJson) ?? new Dictionary<string, string>(),
                Price = v.Price,
                StockQuantity = v.StockQuantity,
                ReservedQuantity = v.ReservedQuantity,
                AvailableQuantity = v.AvailableQuantity
            }).ToList();

            if (dto.Variants.Any())
            {
                dto.Price = dto.Variants.Min(v => v.Price);
            }
            result.Add(dto);
        }

        return result;
    }

    public async Task<bool> ToggleWishlistAsync(Guid userId, Guid productId)
    {
        var existing = (await _unitOfWork.Repository<WishlistItem>().FindAsync(w => w.UserId == userId && w.ProductId == productId)).FirstOrDefault();
        
        if (existing != null)
        {
            if (existing.IsDeleted)
            {
                existing.IsDeleted = false;
                existing.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Repository<WishlistItem>().Update(existing);
            }
            else
            {
                // Hard delete or Soft delete
                _unitOfWork.Repository<WishlistItem>().Delete(existing);
            }
        }
        else
        {
            var newItem = new WishlistItem
            {
                UserId = userId,
                ProductId = productId,
            };
            await _unitOfWork.Repository<WishlistItem>().AddAsync(newItem);
        }

        await _unitOfWork.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CheckWishlistAsync(Guid userId, Guid productId)
    {
        var existing = (await _unitOfWork.Repository<WishlistItem>().FindAsync(w => w.UserId == userId && w.ProductId == productId && !w.IsDeleted)).FirstOrDefault();
        return existing != null;
    }
}
