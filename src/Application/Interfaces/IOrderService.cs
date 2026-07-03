using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<Guid> CreateOrderAsync(CreateOrderRequest request, Guid userId);
    Task<bool> CompleteOrderAsync(Guid orderId);
    Task<IEnumerable<Order>> GetMyOrdersAsync(Guid userId);
    Task<bool> CancelOrderAsync(Guid orderId);
    Task<bool> ProcessPaymentAsync(Guid orderId, bool simulateSuccess);
    Task<Order?> GetOrderByIdAsync(Guid orderId);

    // Admin methods
    Task<IEnumerable<Order>> GetAllOrdersAsync(string? statusFilter = null);
    Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus);
    
    // Return & Refund
    Task<ReturnRequestDto> RequestReturnAsync(Guid userId, CreateReturnRequest request);
    Task<List<ReturnRequestDto>> GetReturnRequestsAsync();
    Task<bool> ProcessReturnRequestAsync(Guid returnRequestId, ProcessReturnRequest request);
}
