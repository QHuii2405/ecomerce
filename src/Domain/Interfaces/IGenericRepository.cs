using System.Linq.Expressions;
using Domain.Common;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync(string includeProperties = "");
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, string includeProperties = "");
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}
