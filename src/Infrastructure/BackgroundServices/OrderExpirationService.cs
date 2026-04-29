using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundServices;

public class OrderExpirationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderExpirationService> _logger;

    public OrderExpirationService(IServiceProvider serviceProvider, ILogger<OrderExpirationService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("OrderExpirationService đang quét các đơn hàng quá hạn...");

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Tìm các đơn Pending đã tạo quá 2 giờ 
                var expirationTime = DateTime.UtcNow.AddHours(-2);
                // var expirationTime = DateTime.UtcNow.AddMinutes(-1); // Giảm thời gian để dễ test hơn
                var expiredOrders = await context.Orders
                    .Include(o => o.OrderItems)
                    .Where(o => o.Status == "Pending" && o.CreatedAt < expirationTime)
                    .ToListAsync();

                foreach (var order in expiredOrders)
                {
                    try
                    {
                        order.Status = "Cancelled";
                        order.Note = "Tự động hủy do quá hạn thanh toán (2 giờ).";

                        // Hoàn trả kho hàng [cite: 88]
                        foreach (var item in order.OrderItems)
                        {
                            var inventory = await context.Inventories
                                .FirstOrDefaultAsync(i => i.ProductId == item.ProductId);
                            if (inventory != null)
                            {
                                inventory.ReservedQuantity -= item.Quantity;
                            }
                        }

                        _logger.LogWarning($"Đã tự động hủy đơn hàng: {order.Id}");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Lỗi khi hủy đơn {order.Id}: {ex.Message}");
                    }
                }

                await context.SaveChangesAsync();
            }

            // Chờ 30 phút trước khi quét lại lần tiếp theo 
            await Task.Delay(TimeSpan.FromMinutes(30), stoppingToken);
            // await Task.Delay(TimeSpan.FromSeconds(20), stoppingToken);
        }
    }
}