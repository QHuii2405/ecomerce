---
name: redis-concurrency
description: Hướng dẫn tích hợp Redis Caching resilient (bền bỉ) và xử lý tranh chấp kho hàng đồng thời cao (Inventory Concurrency) đạt chuẩn vận hành môi trường Production.
tags: [redis, caching, concurrency, sql-server, dotnet, backend, production-ready]
---

# ⚡ Kỹ năng: Tối ưu Caching Redis & Xử lý Tranh chấp Kho hàng (Concurrency) chuyên sâu

Kỹ năng này hướng dẫn AI Agent áp dụng các kỹ thuật nâng cao đạt chuẩn vận hành môi trường Production để tăng tốc ứng dụng thông qua Redis Cache bền bỉ và bảo vệ dữ liệu tồn kho dưới áp lực truy cập đồng thời siêu cao (High Concurrency).

---

## 🚀 1. Redis Caching bền bỉ (Resilient Caching)

Trong môi trường Production, Redis có thể bị quá tải hoặc mất kết nối tạm thời. Ứng dụng **tuyệt đối không được crash** khi Redis gặp sự cố. Chúng ta phải áp dụng cơ chế **Fallback (Dự phòng)** về cơ sở dữ liệu gốc.

### Quy tắc Vàng: Bao bọc thao tác Redis trong Try-Catch
Mọi lệnh đọc ghi Redis thông qua `IDistributedCache` hoặc `IConnectionMultiplexer` bắt buộc phải được bọc trong khối `try-catch`. Nếu có lỗi xảy ra (ví dụ: mất kết nối Redis), hãy ghi lại cảnh báo (`_logger.LogWarning`) và tiếp tục truy vấn trực tiếp từ SQL Server:

```csharp
public async Task<IEnumerable<Product>> GetProductsAsync()
{
    string cacheKey = "products_all";
    
    // 1. Thử lấy dữ liệu từ Redis với cơ chế bảo vệ (Resilience)
    try
    {
        var cachedData = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            return JsonSerializer.Deserialize<List<Product>>(cachedData) ?? new List<Product>();
        }
    }
    catch (Exception ex)
    {
        // Ghi nhận lỗi nhưng KHÔNG làm dừng luồng chạy của ứng dụng
        _logger.LogError(ex, "Lỗi kết nối Redis khi đọc cache key: {CacheKey}. Chuyển hướng truy vấn sang SQL Server.", cacheKey);
    }

    // 2. Fallback: Lấy dữ liệu trực tiếp từ SQL Server
    var products = await _unitOfWork.Products.GetProductsWithInventoryAsync();

    // 3. Ghi lại vào Redis (nếu Redis hoạt động bình thường)
    try
    {
        var cacheOptions = new DistributedCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));
            
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(products), cacheOptions);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Lỗi kết nối Redis khi ghi cache key: {CacheKey}.", cacheKey);
    }

    return products;
}
```

---

## 🔒 2. Xử lý Tranh chấp Kho hàng đồng thời cao (High Concurrency)

Hệ thống eCommerce có thể đối mặt với hàng vạn yêu cầu đặt hàng cùng một lúc khi mở bán khuyến mãi (Flash Sale). Việc xử lý tranh chấp (concurrency control) là tối quan trọng để ngăn chặn **Overselling** (bán quá số lượng thực tế).

### A. Phương pháp 1: Cập nhật nguyên tử mức Database (Atomic Update - Khuyên dùng)
Hệ thống sử dụng câu lệnh SQL cập nhật nguyên tử có điều kiện trực tiếp dưới Database thông qua `IInventoryRepository`. Đây là phương pháp hiệu năng cao nhất vì nó tận dụng cơ chế khóa hàng (row-lock) tối ưu của SQL Server:
```csharp
var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
    @"UPDATE Inventories 
      SET ReservedQuantity = ReservedQuantity + {1}
      WHERE ProductId = {0} 
      AND (StockQuantity - ReservedQuantity) >= {1}",
    productId, quantity);
```
- **Ưu điểm**: Tốc độ cực nhanh, không cần khóa toàn bộ bảng, xử lý đồng thời tốt.

### B. Phương pháp 2: Khóa phân tán bằng Redis (Distributed Lock)
Đối với các nghiệp vụ phức tạp liên quan đến nhiều hệ thống (ví dụ: vừa kiểm tra tồn kho, vừa áp dụng mã giảm giá giới hạn lượt dùng, vừa gọi API cổng thanh toán), cơ chế khóa của SQL Server là chưa đủ. Chúng ta cần sử dụng **Khóa phân tán (Distributed Lock)** bằng Redis (thường dùng thư viện **RedLock.net**):

```csharp
// Khóa theo ProductId để đảm bảo tại một thời điểm chỉ có 1 request được xử lý cho sản phẩm này
string lockKey = $"lock_product_{productId}";
TimeSpan expiry = TimeSpan.FromSeconds(10);
TimeSpan wait = TimeSpan.FromSeconds(2);

// Thực hiện khóa phân tán qua RedLock
using (var redLock = await _redisLockFactory.CreateLockAsync(lockKey, expiry, wait, retry))
{
    if (redLock.IsAcquired)
    {
        // 1. Đã lấy được khóa -> Thực hiện kiểm tra kho và thanh toán an toàn
        var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(productId);
        // ... thực hiện logic nghiệp vụ ...
        await _unitOfWork.SaveChangesAsync();
    }
    else
    {
        // 2. Không lấy được khóa sau thời gian chờ -> Hệ thống bận
        throw new TimeoutException("Hệ thống đang bận xử lý giao dịch cho sản phẩm này. Vui lòng thử lại.");
    }
}
```
- **Quy tắc**: Chỉ áp dụng Distributed Lock cho các luồng nghiệp vụ thực sự nhạy cảm và phức tạp. Tránh lạm dụng để không làm giảm thông lượng (throughput) tổng thể của hệ thống.
