using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<IEnumerable<Product>> GetProductsWithInventoryAsync();
    Task<Product?> GetProductWithDetailsAsync(Guid id);
}
