using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Exceptions;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously finds entities by ID or throws an exception if not found.</summary>
    /// <param name="id">The ID of the entities to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of entities matching the specified ID.</returns>
    /// <exception cref="BaseException">Thrown if no entities are found.</exception>
    public async Task<IEnumerable<TEntity>> FindManyOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity>? entities = await databaseContext.Set<TEntity>().AsNoTracking()
            .Where(entity => entity.Id == id && !entity.ExcludedAt.HasValue)
            .ToListAsync(cancellationToken);

        return entities.Any()
            ? entities
            : throw new BaseException(localizationService, LocalizationMessages.EntityNotFound);
    }

    /// <summary>Asynchronously finds entities matching the specified predicate or throws an exception if not found.</summary>
    /// <param name="predicate">The predicate to match the entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of entities matching the specified predicate.</returns>
    /// <exception cref="BaseException">Thrown if no entities are found.</exception>
    public async Task<IEnumerable<TEntity>> FindManyOrThrowAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity>? entities = await databaseContext.Set<TEntity>().AsNoTracking()
            .Where(entity => !entity.ExcludedAt.HasValue)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        return entities.Any()
            ? entities
            : throw new BaseException(localizationService, LocalizationMessages.EntityNotFound);
    }
}
