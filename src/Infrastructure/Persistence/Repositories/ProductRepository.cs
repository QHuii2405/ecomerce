using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsWithInventoryAsync()
    {
        return await _dbSet
            .Include(p => p.Inventory)
            .Include(p => p.Category)
            .Include(p => p.Variants.Where(v => !v.IsDeleted && v.IsActive))
            .Include(p => p.Reviews.Where(r => !r.IsDeleted))
                .ThenInclude(r => r.User)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<Product?> GetProductWithDetailsAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Inventory)
            .Include(p => p.Category)
            .Include(p => p.Variants.Where(v => !v.IsDeleted && v.IsActive))
            .Include(p => p.Reviews.Where(r => !r.IsDeleted))
                .ThenInclude(r => r.User)
            .FirstOrDefaultAsync(p => !p.IsDeleted && p.Id == id);
    }
}
