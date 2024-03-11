using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously checks if an entity with the specified ID exists and is not excluded.</summary>
    /// <param name="id">The ID of the entity to check.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the entity exists and is not excluded, false otherwise.</returns>
    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking()
            .FirstOrDefaultAsync(
                entity => entity.Id == id && !entity.ExcludedAt.HasValue,
                cancellationToken
            );

        return entity is not null;
    }

    /// <summary>Asynchronously checks if an entity matching the specified predicate exists and is not excluded.</summary>
    /// <param name="predicate">The predicate to match the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the entity exists and is not excluded, false otherwise.</returns>
    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking()
            .Where(entity => !entity.ExcludedAt.HasValue)
            .FirstOrDefaultAsync(predicate, cancellationToken);

        return entity is not null;
    }
}
