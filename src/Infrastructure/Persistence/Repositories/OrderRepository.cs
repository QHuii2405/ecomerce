using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Order?> GetOrderWithItemsAsync(Guid id)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserIdWithItemsAsync(Guid userId)
    {
        return await _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Order>> GetAllOrdersWithDetailsAsync(string? statusFilter = null)
    {
        var query = _dbSet
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .AsQueryable();

        if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
        {
            query = query.Where(o => o.Status == statusFilter);
        }

        return await query
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
}
