using System.Linq.Expressions;

using Portfolio.Entities;

namespace Portfolio.Utils.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default);

    Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default);

    Task Remove(T? entity, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
