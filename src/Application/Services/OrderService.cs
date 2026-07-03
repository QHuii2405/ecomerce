using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly INotificationService _notificationService;
    private readonly IPdfService _pdfService;
    private readonly IPaymentGatewayService _paymentService;

    public OrderService(
        IUnitOfWork unitOfWork, 
        IEmailService emailService, 
        INotificationService notificationService, 
        IPdfService pdfService,
        IPaymentGatewayService paymentService)
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _notificationService = notificationService;
        _pdfService = pdfService;
        _paymentService = paymentService;
    }

    public async Task<Order?> GetOrderByIdAsync(Guid orderId)
    {
        return await _unitOfWork.Orders.GetByIdAsync(orderId);
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
            // Apply Voucher if provided
            if (request.VoucherId.HasValue)
            {
                var voucher = await _unitOfWork.Repository<Voucher>().GetByIdAsync(request.VoucherId.Value);
                if (voucher != null && voucher.IsActive && voucher.ExpiryDate >= DateTime.UtcNow && voucher.UsedCount < voucher.UsageLimit && order.TotalAmount >= voucher.MinOrderValue)
                {
                    decimal discount = voucher.DiscountType == "Percentage" 
                        ? order.TotalAmount * (voucher.DiscountValue / 100) 
                        : voucher.DiscountValue;
                        
                    if (voucher.MaxDiscountValue.HasValue && discount > voucher.MaxDiscountValue.Value)
                    {
                        discount = voucher.MaxDiscountValue.Value;
                    }

                    if (discount > order.TotalAmount) discount = order.TotalAmount;

                    order.DiscountAmount = discount;
                    order.VoucherId = voucher.Id;
                    
                    voucher.UsedCount += 1;
                    _unitOfWork.Repository<Voucher>().Update(voucher);
                }
            }

            await _unitOfWork.Orders.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            var user = await _unitOfWork.Users.GetByIdAsync(userId);
            if (user != null)
            {
                var subject = $"Xác nhận đơn hàng #{order.Id.ToString().Substring(0, 8)}";
                var body = $"<h1>Cảm ơn {user.FullName} đã đặt hàng!</h1><p>Mã đơn hàng: {order.Id}</p><p>Tổng tiền: {order.TotalAmount:N0}đ</p>";
                
                byte[]? pdfBytes = null;
                try 
                {
                    pdfBytes = _pdfService.GenerateInvoicePdf(order);
                }
                catch 
                {
                    // If PDF generation fails, continue without PDF
                }

                _ = _emailService.SendEmailAsync(user.Email, subject, body, pdfBytes, pdfBytes != null ? $"HoaDon_{order.Id.ToString().Substring(0, 8)}.pdf" : null);
            }
            _ = _notificationService.SendNotificationToAdminsAsync("Đơn hàng mới", $"Đơn hàng #{order.Id.ToString().Substring(0, 8)} vừa được đặt!");

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

            // Tích điểm & Cập nhật hạng thành viên
            var user = await _unitOfWork.Users.GetByIdAsync(order.UserId);
            if (user != null)
            {
                user.TotalSpent += order.TotalAmount;
                user.LoyaltyPoints += (int)(order.TotalAmount / 10000); // 10,000đ = 1 điểm

                if (user.TotalSpent >= 50000000) user.MembershipTier = "Gold";
                else if (user.TotalSpent >= 10000000) user.MembershipTier = "Silver";
                else user.MembershipTier = "Bronze";

                _unitOfWork.Users.Update(user);
            }

            await _unitOfWork.CommitTransactionAsync();

            if (user != null)
            {
                _ = _emailService.SendEmailAsync(user.Email, "Đơn hàng đã giao", $"Đơn hàng #{order.Id.ToString().Substring(0, 8)} đã được giao thành công. Bạn được cộng {(int)(order.TotalAmount / 10000)} điểm thưởng!");
                _ = _notificationService.SendNotificationToUserAsync(user.Id.ToString(), "Đã giao hàng", $"Đơn hàng #{order.Id.ToString().Substring(0, 8)} đã được giao thành công.");
            }

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
            }

            // Nếu hủy đơn thì hoàn trả kho
            if (newStatus == "Cancelled" && (order.Status == "Pending" || order.Status == "Confirmed"))
            {
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
                            inventory.ReservedQuantity = Math.Max(0, inventory.ReservedQuantity - item.Quantity);
                            _unitOfWork.Inventories.Update(inventory);
                        }
                    }
                }
            }

            order.Status = newStatus;
            order.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Orders.Update(order);
            await _unitOfWork.CommitTransactionAsync();

            var user = await _unitOfWork.Users.GetByIdAsync(order.UserId);
            if (user != null)
            {
                _ = _notificationService.SendNotificationToUserAsync(user.Id.ToString(), "Cập nhật đơn hàng", $"Đơn hàng #{order.Id.ToString().Substring(0, 8)} đã chuyển sang trạng thái: {newStatus}");
                
                if (newStatus == "Cancelled")
                {
                    _ = _emailService.SendEmailAsync(user.Email, "Đơn hàng đã bị hủy", $"Đơn hàng #{order.Id.ToString().Substring(0, 8)} của bạn đã bị hủy.");
                }
            }
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }

    // ==================== RETURN / REFUND METHODS ====================

    public async Task<ReturnRequestDto> RequestReturnAsync(Guid userId, CreateReturnRequest request)
    {
        var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);
        if (order == null) throw new KeyNotFoundException("Đơn hàng không tồn tại.");
        if (order.UserId != userId) throw new UnauthorizedAccessException("Bạn không có quyền thực hiện trên đơn hàng này.");
        if (order.Status != "Delivered") throw new InvalidOperationException("Chỉ đơn hàng đã giao mới có thể yêu cầu hoàn trả.");

        var existingRequest = await _unitOfWork.Repository<ReturnRequest>()
            .FindAsync(r => r.OrderId == request.OrderId && r.Status == "Pending");
        if (existingRequest.Any())
        {
            throw new InvalidOperationException("Đơn hàng này đã có yêu cầu hoàn trả đang chờ xử lý.");
        }

        var returnRequest = new ReturnRequest
        {
            OrderId = request.OrderId,
            Reason = request.Reason,
            ImageUrls = request.ImageUrls != null && request.ImageUrls.Any() ? System.Text.Json.JsonSerializer.Serialize(request.ImageUrls) : null,
            Status = "Pending",
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<ReturnRequest>().AddAsync(returnRequest);
        
        order.Status = "ReturnRequested";
        order.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.Orders.Update(order);
        
        await _unitOfWork.SaveChangesAsync();

        _ = _notificationService.SendNotificationToAdminsAsync("Yêu cầu hoàn trả", $"Đơn hàng #{order.Id.ToString().Substring(0, 8)} vừa có yêu cầu hoàn trả/hoàn tiền.");

        return new ReturnRequestDto
        {
            Id = returnRequest.Id,
            OrderId = returnRequest.OrderId,
            Reason = returnRequest.Reason,
            ImageUrls = request.ImageUrls,
            Status = returnRequest.Status,
            CreatedAt = returnRequest.CreatedAt
        };
    }

    public async Task<List<ReturnRequestDto>> GetReturnRequestsAsync()
    {
        var requests = await _unitOfWork.Repository<ReturnRequest>()
            .GetAllAsync(includeProperties: "Order");
            
        return requests.OrderByDescending(r => r.CreatedAt).Select(r => new ReturnRequestDto
        {
            Id = r.Id,
            OrderId = r.OrderId,
            Reason = r.Reason,
            ImageUrls = !string.IsNullOrEmpty(r.ImageUrls) ? System.Text.Json.JsonSerializer.Deserialize<List<string>>(r.ImageUrls) : null,
            Status = r.Status,
            AdminNote = r.AdminNote,
            CreatedAt = r.CreatedAt
        }).ToList();
    }

    public async Task<bool> ProcessReturnRequestAsync(Guid returnRequestId, ProcessReturnRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();
        try
        {
            var returnRequest = await _unitOfWork.Repository<ReturnRequest>().GetByIdAsync(returnRequestId);
            if (returnRequest == null) throw new KeyNotFoundException("Yêu cầu hoàn trả không tồn tại.");
            if (returnRequest.Status != "Pending") throw new InvalidOperationException("Yêu cầu này đã được xử lý.");

            var order = await _unitOfWork.Orders.GetOrderWithItemsAsync(returnRequest.OrderId);
            if (order == null) throw new KeyNotFoundException("Đơn hàng không tồn tại.");

            var user = await _unitOfWork.Users.GetByIdAsync(order.UserId);

            if (request.Approve)
            {
                // Approve
                returnRequest.Status = "Approved";
                returnRequest.AdminNote = request.AdminNote;
                returnRequest.UpdatedAt = DateTime.UtcNow;

                // Refund payment if it was paid
                if (order.PaymentStatus == "Paid" || order.PaymentStatus == "Refunded")
                {
                    // Refund through gateway (Mocked in PaymentGatewayService)
                    await _paymentService.RefundPaymentAsync(order.Id);
                    returnRequest.Status = "Refunded";
                }

                order.Status = returnRequest.Status == "Refunded" ? "Refunded" : "Returned";
                order.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Orders.Update(order);

                // Restore Inventory / Stock
                foreach (var item in order.OrderItems)
                {
                    if (item.ProductVariantId.HasValue)
                    {
                        var variant = await _unitOfWork.Repository<ProductVariant>().GetByIdAsync(item.ProductVariantId.Value);
                        if (variant != null)
                        {
                            variant.StockQuantity += item.Quantity;
                            variant.UpdatedAt = DateTime.UtcNow;
                            _unitOfWork.Repository<ProductVariant>().Update(variant);
                        }
                    }
                    else
                    {
                        var inventory = await _unitOfWork.Inventories.GetByProductIdAsync(item.ProductId);
                        if (inventory != null)
                        {
                            inventory.StockQuantity += item.Quantity;
                            _unitOfWork.Inventories.Update(inventory);
                        }
                    }
                }

                _unitOfWork.Repository<ReturnRequest>().Update(returnRequest);
                await _unitOfWork.CommitTransactionAsync();

                if (user != null)
                {
                    _ = _emailService.SendEmailAsync(user.Email, "Yêu cầu hoàn trả được chấp nhận", $"Yêu cầu hoàn trả cho đơn hàng #{order.Id.ToString().Substring(0, 8)} đã được chấp nhận. {request.AdminNote}");
                    _ = _notificationService.SendNotificationToUserAsync(user.Id.ToString(), "Hoàn trả thành công", $"Yêu cầu hoàn trả cho đơn hàng #{order.Id.ToString().Substring(0, 8)} đã được duyệt.");
                }

                return true;
            }
            else
            {
                // Reject
                returnRequest.Status = "Rejected";
                returnRequest.AdminNote = request.AdminNote;
                returnRequest.UpdatedAt = DateTime.UtcNow;

                order.Status = "Delivered"; // Revert back to delivered
                order.UpdatedAt = DateTime.UtcNow;

                _unitOfWork.Repository<ReturnRequest>().Update(returnRequest);
                _unitOfWork.Orders.Update(order);
                await _unitOfWork.CommitTransactionAsync();

                if (user != null)
                {
                    _ = _emailService.SendEmailAsync(user.Email, "Yêu cầu hoàn trả bị từ chối", $"Yêu cầu hoàn trả cho đơn hàng #{order.Id.ToString().Substring(0, 8)} bị từ chối. Lý do: {request.AdminNote}");
                    _ = _notificationService.SendNotificationToUserAsync(user.Id.ToString(), "Hoàn trả thất bại", $"Yêu cầu hoàn trả cho đơn hàng #{order.Id.ToString().Substring(0, 8)} bị từ chối.");
                }

                return false;
            }
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
