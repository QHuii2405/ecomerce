using Domain.Entities;

namespace Domain.Interfaces;

public interface IInventoryRepository : IGenericRepository<Inventory>
{
    Task<Inventory?> GetByProductIdAsync(Guid productId);
    Task<bool> ReserveStockAsync(Guid productId, int quantity);
}
