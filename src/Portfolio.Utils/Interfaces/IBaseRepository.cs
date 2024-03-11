using System.Linq.Expressions;

using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FindManyOrThrowAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FindManyOrThrowAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<TEntity> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default);

    Task<TEntity> FindOneOrThrowAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task RemoveHardAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveSoftAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}
