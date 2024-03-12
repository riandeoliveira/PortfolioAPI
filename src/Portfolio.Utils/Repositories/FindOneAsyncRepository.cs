using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously finds a single entity with the specified ID that is not excluded.</summary>
    /// <param name="id">The ID of the entity to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entity that matches the specified ID and is not excluded, or null if no such entity exists.</returns>
    public async Task<TEntity?> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking()
            .FirstOrDefaultAsync(
                entity => entity.Id == id && !entity.ExcludedAt.HasValue,
                cancellationToken
            );

        return entity;
    }

    /// <summary>Asynchronously finds a single entity that matches the specified predicate and is not excluded.</summary>
    /// <param name="predicate">The predicate to match the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entity that matches the specified predicate and is not excluded, or null if no such entity exists.</returns>
    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking()
            .Where(entity => !entity.ExcludedAt.HasValue)
            .FirstOrDefaultAsync(predicate, cancellationToken);

        return entity;
    }
}
