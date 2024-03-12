using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously finds entities with the specified ID that are not excluded.</summary>
    /// <param name="id">The ID of the entities to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of entities that match the specified ID and are not excluded.</returns>
    public async Task<IEnumerable<TEntity>> FindManyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await databaseContext.Set<TEntity>().AsNoTracking()
            .Where(entity => entity.Id == id && !entity.ExcludedAt.HasValue)
            .ToListAsync(cancellationToken);

        return entities;
    }

    /// <summary>Asynchronously finds entities that match the specified predicate and are not excluded.</summary>
    /// <param name="predicate">The predicate to match the entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of entities that match the specified predicate and are not excluded.</returns>
    public async Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await databaseContext.Set<TEntity>().AsNoTracking()
            .Where(entity => !entity.ExcludedAt.HasValue)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        return entities;
    }
}
