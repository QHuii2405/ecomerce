using Domain.Entities;

namespace Domain.Interfaces;

public interface IGoodsReceiptRepository : IGenericRepository<GoodsReceipt>
{
    Task<IEnumerable<GoodsReceipt>> GetReceiptsWithDetailsAsync(int pageIndex, int pageSize);
    Task<int> CountAsync();
    Task<GoodsReceipt?> GetReceiptWithDetailsByIdAsync(Guid id);
}
