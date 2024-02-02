using System.Linq.Expressions;

using Portfolio.Entities;

namespace Portfolio.Utils.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity);

    Task DeleteAsync(T entity);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> GetAsync();

    Task SaveChangesAsync();
}
