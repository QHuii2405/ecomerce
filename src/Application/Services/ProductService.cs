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

    public async Task<(IEnumerable<Product> Products, string Source)> GetAllProductsAsync()
    {
        // 1. Thử lấy dữ liệu từ Redis Cache
        var cachedProducts = await _cache.GetStringAsync(CacheKey);
        if (!string.IsNullOrEmpty(cachedProducts))
        {
            var productsFromCache = JsonSerializer.Deserialize<List<Product>>(cachedProducts);
            if (productsFromCache != null)
            {
                return (productsFromCache, "Redis Cache");
            }
        }

        // 2. Nếu không có trong Cache, lấy từ SQL Server thông qua Repository
        var products = await _unitOfWork.Products.GetProductsWithInventoryAsync();

        // 3. Lưu kết quả vào Redis với TTL là 5 phút
        var cacheOptions = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        var serializedProducts = JsonSerializer.Serialize(products);
        await _cache.SetStringAsync(CacheKey, serializedProducts, cacheOptions);

        return (products, "SQL Server Database");
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
                CategoryId = request.CategoryId
            };

            await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            // Tự động khởi tạo kho hàng cho sản phẩm mới
            var inventory = new Inventory
            {
                ProductId = product.Id,
                StockQuantity = request.InitialStock,
                ReservedQuantity = 0
            };

            await _unitOfWork.Inventories.AddAsync(inventory);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            // Vô hiệu hóa cache cũ
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

            await _unitOfWork.CommitTransactionAsync();
            await _cache.RemoveAsync(CacheKey);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }}
