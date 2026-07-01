using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> CreateOrderAsync(CreateOrderRequest request, Guid userId)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var order = new Order
            {
                UserId = userId,
                Status = "Pending",
                TotalAmount = 0,
                Note = request.Note,
                ShippingAddress = request.ShippingAddress,
                RecipientName = request.RecipientName,
                RecipientPhone = request.RecipientPhone,
                PaymentMethod = request.PaymentMethod
            };

            foreach (var item in request.Items)
            {
                var product = await _unitOfWork.Products.GetProductWithDetailsAsync(item.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException($"Sản phẩm {item.ProductId} không tồn tại trong hệ thống!");
                }

                ProductVariant? variant = null;
                if (item.ProductVariantId.HasValue)
                {
                    variant = product.Variants.FirstOrDefault(v => v.Id == item.ProductVariantId.Value && !v.IsDeleted && v.IsActive);
                    if (variant == null)
                    {
                        throw new InvalidOperationException("Biến thể sản phẩm không tồn tại hoặc đã ngừng bán.");
                    }

                    if (variant.AvailableQuantity < item.Quantity)
                    {
                        throw new InvalidOperationException($"Biến thể {variant.Name} không đủ hàng!");
                    }

                    variant.ReservedQuantity += item.Quantity;
                    variant.UpdatedAt = DateTime.UtcNow;
                    _unitOfWork.Repository<ProductVariant>().Update(variant);
                }
                else
                {
                    var isReserved = await _unitOfWork.Inventories.ReserveStockAsync(item.ProductId, item.Quantity);
                    if (!isReserved)
                    {
                        throw new InvalidOperationException($"Sản phẩm {item.ProductId} không đủ hàng hoặc đã bị đặt bởi người khác!");
                    }
                }

                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    UnitPrice = variant?.Price ?? product.Price,
                    VariantSnapshotJson = variant?.AttributesJson ?? "{}"
                };
                order.OrderItems.Add(orderItem);
                order.TotalAmount += orderItem.UnitPrice * orderItem.Quantity;
            }

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return order.Id;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> CompleteOrderAsync(Guid orderId)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var order = await _unitOfWork.Orders.GetOrderWithItemsAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Đơn hàng không tồn tại.");
            if (order.Status != "Shipping")
                throw new InvalidOperationException("Chỉ có thể hoàn tất các đơn đang được giao.");

            foreach (var item in order.OrderItems)
            {
                if (item.ProductVariantId.HasValue)
                {
                    var variant = await _unitOfWork.Repository<ProductVariant>().GetByIdAsync(item.ProductVariantId.Value);
                    if (variant != null)
                    {
                        variant.StockQuantity -= item.Quantity;
                        variant.ReservedQuantity -= item.Quantity;
                        variant.UpdatedAt = DateTime.UtcNow;
                        _unitOfWork.Repository<ProductVariant>().Update(variant);
                    }
                }
                else
                {
                    var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(item.ProductId);
                    if (inventory != null)
                    {
                        inventory.StockQuantity -= item.Quantity;
                        inventory.ReservedQuantity -= item.Quantity;
                        _unitOfWork.Inventories.Update(inventory);
                    }
                }
            }

            order.Status = "Delivered";
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IEnumerable<Order>> GetMyOrdersAsync(Guid userId)
    {
        return await _unitOfWork.Orders.GetOrdersByUserIdWithItemsAsync(userId);
    }

    public async Task<bool> CancelOrderAsync(Guid orderId)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var order = await _unitOfWork.Orders.GetOrderWithItemsAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Đơn hàng không tồn tại.");

            if (order.Status != "Pending" && order.Status != "Confirmed")
                throw new InvalidOperationException("Không thể hủy đơn hàng ở trạng thái này.");

            foreach (var item in order.OrderItems)
            {
                if (item.ProductVariantId.HasValue)
                {
                    var variant = await _unitOfWork.Repository<ProductVariant>().GetByIdAsync(item.ProductVariantId.Value);
                    if (variant != null)
                    {
                        variant.ReservedQuantity = Math.Max(0, variant.ReservedQuantity - item.Quantity);
                        variant.UpdatedAt = DateTime.UtcNow;
                        _unitOfWork.Repository<ProductVariant>().Update(variant);
                    }
                }
                else
                {
                    var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(item.ProductId);
                    if (inventory != null)
                    {
                        inventory.ReservedQuantity -= item.Quantity;
                        _unitOfWork.Inventories.Update(inventory);
                    }
                }
            }

            order.Status = "Cancelled";
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<bool> ProcessPaymentAsync(Guid orderId, bool simulateSuccess)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(orderId);
        if (order == null) throw new KeyNotFoundException("Không tìm thấy đơn hàng.");
        if (order.Status != "Pending")
            throw new InvalidOperationException("Đơn hàng không ở trạng thái chờ thanh toán.");

        if (simulateSuccess)
        {
            order.Status = "Confirmed";
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }

    // ==================== ADMIN METHODS ====================

    public async Task<IEnumerable<Order>> GetAllOrdersAsync(string? statusFilter = null)
    {
        return await _unitOfWork.Orders.GetAllOrdersWithDetailsAsync(statusFilter);
    }

    public async Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus)
    {
        var validStatuses = new[] { "Pending", "Confirmed", "Shipping", "Delivered", "Cancelled" };
        if (!validStatuses.Contains(newStatus))
            throw new ArgumentException($"Trạng thái không hợp lệ: {newStatus}");

        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var order = await _unitOfWork.Orders.GetOrderWithItemsAsync(orderId);
            if (order == null) throw new KeyNotFoundException("Đơn hàng không tồn tại.");

            // Nếu chuyển sang Delivered thì trừ kho thực tế
            if (newStatus == "Delivered" && order.Status == "Shipping")
            {
                foreach (var item in order.OrderItems)
                {
                    var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(item.ProductId);
                    if (inventory != null)
                    {
                        inventory.StockQuantity -= item.Quantity;
                        inventory.ReservedQuantity -= item.Quantity;
                        _unitOfWork.Inventories.Update(inventory);
                    }
                }
            }

            // Nếu hủy đơn thì hoàn trả kho
            if (newStatus == "Cancelled" && (order.Status == "Pending" || order.Status == "Confirmed"))
            {
                foreach (var item in order.OrderItems)
                {
                    var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(item.ProductId);
                    if (inventory != null)
                    {
                        inventory.ReservedQuantity -= item.Quantity;
                        _unitOfWork.Inventories.Update(inventory);
                    }
                }
            }

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
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
