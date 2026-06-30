using Domain.Entities;

namespace Domain.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
    Task<Order?> GetOrderWithItemsAsync(Guid id);
    Task<IEnumerable<Order>> GetOrdersByUserIdWithItemsAsync(Guid userId);
    Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync(string? statusFilter = null);
}
