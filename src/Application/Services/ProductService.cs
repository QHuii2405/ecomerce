using System.Text.Json;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDistributedCache _cache;
    private const string CacheKey = "products_all";

    public ProductService(IUnitOfWork unitOfWork, IDistributedCache cache)
    {
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<(IEnumerable<ProductResponse> Products, string Source)> GetAllProductsAsync()
    {
        var cachedProducts = await _cache.GetStringAsync(CacheKey);
        if (!string.IsNullOrEmpty(cachedProducts))
        {
            var productsFromCache = JsonSerializer.Deserialize<List<ProductResponse>>(cachedProducts);
            if (productsFromCache != null)
            {
                return (productsFromCache, "Redis Cache");
            }
        }

        var products = await _unitOfWork.Products.GetProductsWithInventoryAsync();
        var productResponses = products.Select(MapToProductResponse).ToList();

        var cacheOptions = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        var serializedProducts = JsonSerializer.Serialize(productResponses);
        await _cache.SetStringAsync(CacheKey, serializedProducts, cacheOptions);

        return (productResponses, "SQL Server Database");
    }

    public async Task<ProductResponse?> GetProductByIdAsync(Guid id)
    {
        var product = await _unitOfWork.Products.GetProductWithDetailsAsync(id);
        return product == null ? null : MapToProductResponse(product);
    }

    public async Task<Guid> CreateProductAsync(CreateProductRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId,
                Brand = request.Brand,
                AttributesJson = SerializeDictionary(request.Attributes)
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            var inventory = new Inventory
            {
                ProductId = product.Id,
                StockQuantity = request.InitialStock,
                ReservedQuantity = 0
            };

            await _unitOfWork.Inventories.AddAsync(inventory);

            foreach (var variantRequest in request.Variants)
            {
                await _unitOfWork.Repository<ProductVariant>().AddAsync(new ProductVariant
                {
                    ProductId = product.Id,
                    Sku = variantRequest.Sku,
                    Name = variantRequest.Name,
                    AttributesJson = SerializeDictionary(variantRequest.Attributes),
                    Price = variantRequest.Price,
                    StockQuantity = variantRequest.StockQuantity,
                    ReservedQuantity = 0,
                    IsActive = true
                });
            }

            await _unitOfWork.CommitTransactionAsync();
            await _cache.RemoveAsync(CacheKey);

            return product.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task UpdateProductAsync(Guid id, UpdateProductRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null || product.IsDeleted)
            {
                throw new KeyNotFoundException("Khong tim thay san pham.");
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.CategoryId = request.CategoryId;
            product.Brand = request.Brand;
            product.AttributesJson = SerializeDictionary(request.Attributes);
            product.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Products.Update(product);

            var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(id);
            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ProductId = id,
                    StockQuantity = request.StockQuantity,
                    ReservedQuantity = 0
                };
                await _unitOfWork.Inventories.AddAsync(inventory);
            }
            else
            {
                inventory.StockQuantity = Math.Max(request.StockQuantity, inventory.ReservedQuantity);
                inventory.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Inventories.Update(inventory);
            }

            await _unitOfWork.CommitTransactionAsync();
            await _cache.RemoveAsync(CacheKey);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var product = await _unitOfWork.Products.GetByIdAsync(id);
            if (product == null || product.IsDeleted)
            {
                throw new KeyNotFoundException("Khong tim thay san pham.");
            }

            product.IsDeleted = true;
            product.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Products.Update(product);

            var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(id);
            if (inventory != null)
            {
                inventory.IsDeleted = true;
                inventory.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Inventories.Update(inventory);
            }

            var variants = await _unitOfWork.Repository<ProductVariant>().FindAsync(v => v.ProductId == id && !v.IsDeleted);
            foreach (var variant in variants)
            {
                variant.IsDeleted = true;
                variant.IsActive = false;
                variant.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Repository<ProductVariant>().Update(variant);
            }

            await _unitOfWork.CommitTransactionAsync();
            await _cache.RemoveAsync(CacheKey);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    private static ProductResponse MapToProductResponse(Product product)
    {
        var reviews = product.Reviews.Where(r => !r.IsDeleted).ToList();

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Brand = product.Brand,
            Attributes = DeserializeDictionary(product.AttributesJson),
            Category = product.Category == null ? null : new CategoryResponse
            {
                Id = product.Category.Id,
                Name = product.Category.Name,
                Description = product.Category.Description,
                ParentId = product.Category.ParentId
            },
            Inventory = product.Inventory == null ? null : new InventoryResponse
            {
                Id = product.Inventory.Id,
                ProductId = product.Inventory.ProductId,
                StockQuantity = product.Inventory.StockQuantity,
                ReservedQuantity = product.Inventory.ReservedQuantity,
                AvailableQuantity = product.Inventory.AvailableQuantity
            },
            Variants = product.Variants
                .Where(v => !v.IsDeleted && v.IsActive)
                .Select(v => new ProductVariantResponse
                {
                    Id = v.Id,
                    ProductId = v.ProductId,
                    Sku = v.Sku,
                    Name = v.Name,
                    Attributes = DeserializeDictionary(v.AttributesJson),
                    Price = v.Price,
                    StockQuantity = v.StockQuantity,
                    ReservedQuantity = v.ReservedQuantity,
                    AvailableQuantity = v.AvailableQuantity
                })
                .ToList(),
            ReviewSummary = new ReviewSummaryResponse
            {
                ReviewCount = reviews.Count,
                AverageRating = reviews.Count == 0 ? 0 : Math.Round(reviews.Average(r => r.Rating), 1)
            }
        };
    }

    private static string SerializeDictionary(Dictionary<string, string> attributes)
    {
        return JsonSerializer.Serialize(attributes ?? new Dictionary<string, string>());
    }

    private static Dictionary<string, string> DeserializeDictionary(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return new Dictionary<string, string>();

        try
        {
            return JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        }
        catch
        {
            return new Dictionary<string, string>();
        }
    }
}
