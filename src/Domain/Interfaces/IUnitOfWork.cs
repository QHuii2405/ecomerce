using Domain.Common;
using Domain.Entities;

namespace Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IInventoryRepository Inventories { get; }
    IOrderRepository Orders { get; }
    IGoodsReceiptRepository GoodsReceipts { get; }
    IGenericRepository<Category> Categories { get; }
    IGenericRepository<User> Users { get; }

    IGenericRepository<T> Repository<T>() where T : BaseEntity;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
