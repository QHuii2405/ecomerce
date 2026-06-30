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

    // Admin methods
    Task<IEnumerable<Order>> GetAllOrdersAsync(string? statusFilter = null);
    Task<bool> UpdateOrderStatusAsync(Guid orderId, string newStatus);
}
