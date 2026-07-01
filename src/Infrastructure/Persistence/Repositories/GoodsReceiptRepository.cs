using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class GoodsReceiptRepository : GenericRepository<GoodsReceipt>, IGoodsReceiptRepository
{
    public GoodsReceiptRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync(r => !r.IsDeleted);
    }

    public async Task<IEnumerable<GoodsReceipt>> GetReceiptsWithDetailsAsync(int pageIndex, int pageSize)
    {
        return await _dbSet
            .Include(r => r.CreatedByUser)
            .Include(r => r.ApprovedByUser)
            .Include(r => r.Details)
                .ThenInclude(d => d.Product)
            .Include(r => r.Details)
                .ThenInclude(d => d.ProductVariant)
            .Where(r => !r.IsDeleted)
            .OrderByDescending(r => r.CreatedAt)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<GoodsReceipt?> GetReceiptWithDetailsByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(r => r.CreatedByUser)
            .Include(r => r.ApprovedByUser)
            .Include(r => r.Details)
                .ThenInclude(d => d.Product)
            .Include(r => r.Details)
                .ThenInclude(d => d.ProductVariant)
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
    }
}
