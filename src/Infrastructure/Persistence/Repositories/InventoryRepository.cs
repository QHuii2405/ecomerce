using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
{
    public InventoryRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Inventory?> GetByProductIdAsync(Guid productId)
    {
        return await _dbSet.FirstOrDefaultAsync(i => i.ProductId == productId);
    }

    public async Task<bool> ReserveStockAsync(Guid productId, int quantity)
    {
        var rowsAffected = await _context.Database.ExecuteSqlRawAsync(
            @"UPDATE Inventories 
              SET ReservedQuantity = ReservedQuantity + {1}
              WHERE ProductId = {0} 
              AND (StockQuantity - ReservedQuantity) >= {1}",
            productId, quantity);

        return rowsAffected > 0;
    }
}
