using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Exceptions;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously finds an entity by ID or throws an exception if not found.</summary>
    /// <param name="id">The ID of the entity to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="BaseException">Thrown if the entity is not found.</exception>
    public async Task<TEntity> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking()
            .FirstOrDefaultAsync(
                entity => entity.Id == id && !entity.ExcludedAt.HasValue,
                cancellationToken
            );

        return entity is not null
            ? entity
            : throw new BaseException(localizationService, LocalizationMessages.EntityNotFound);
    }

    /// <summary>Asynchronously finds an entity matching the specified predicate or throws an exception if not found.</summary>
    /// <param name="predicate">The predicate to match the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The found entity.</returns>
    /// <exception cref="BaseException">Thrown if the entity is not found.</exception>
    public async Task<TEntity> FindOneOrThrowAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking()
            .Where(entity => !entity.ExcludedAt.HasValue)
            .FirstOrDefaultAsync(predicate, cancellationToken);

        return entity is not null
            ? entity
            : throw new BaseException(localizationService, LocalizationMessages.EntityNotFound);
    }
}
