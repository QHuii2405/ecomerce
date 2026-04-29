namespace WebAPI.Controllers;

using Application.DTOs;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;


[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IDistributedCache _cache;

    public ProductsController(ApplicationDbContext context, IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    // 1. Lấy danh sách sản phẩm (Công khai) [cite: 8, 16]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        string cacheKey = "products_all";

        // 1. Thử lấy dữ liệu từ Redis
        var cachedProducts = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedProducts))
        {
            var productsFromCache = JsonSerializer.Deserialize<List<Product>>(cachedProducts);
            return Ok(new { Data = productsFromCache, Source = "Redis Cache" });
        }

        // 2. Nếu không có trong Cache, lấy từ SQL Server
        var products = await _context.Products
            .Include(p => p.Inventory)
            .Where(p => !p.IsDeleted)
            .ToListAsync();

        // 3. Lưu kết quả vào Redis với thời gian sống (TTL) là 5 phút
        var options = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

        var serializedProducts = JsonSerializer.Serialize(products);
        await _cache.SetStringAsync(cacheKey, serializedProducts, options);

        return Ok(new { Data = products, Source = "SQL Server Database" });
    }

    // 2. Thêm sản phẩm mới (Chỉ dành cho Staff/Admin) [cite: 25, 34, 35]
    [HttpPost]
    [Authorize(Roles = "Staff,Admin")]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        // Sử dụng Transaction để đảm bảo: Nếu tạo kho lỗi thì sản phẩm cũng không được tạo 
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CategoryId = request.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Tự động tạo bản ghi Inventory cho sản phẩm này [cite: 94, 109]
            var inventory = new Inventory
            {
                ProductId = product.Id,
                StockQuantity = request.InitialStock,
                ReservedQuantity = 0
            };

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
            await _cache.RemoveAsync("products_all");
            return Ok(new { Message = "Thêm sản phẩm và khởi tạo kho thành công!", ProductId = product.Id });
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return BadRequest("Có lỗi xảy ra khi tạo sản phẩm.");
        }
    }
}