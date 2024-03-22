using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

public abstract class BaseRepository<TEntity>(
    DatabaseContext databaseContext
) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously creates a new entity in the database.</summary>
    /// <param name="entity">The entity to be created.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created entity.</returns>
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await databaseContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

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

    /// <summary>Asynchronously removes an entity from the database permanently.</summary>
    /// <param name="entity">The entity to be removed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task RemoveHardAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => databaseContext.Set<TEntity>().Remove(entity), cancellationToken);
    }

    /// <summary>Asynchronously marks an entity as excluded without deleting it from the database.</summary>
    /// <param name="entity">The entity to be marked as excluded.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task RemoveSoftAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            entity.ExcludedAt = DateTime.Now;

            databaseContext.Set<TEntity>().Update(entity);
        }, cancellationToken);
    }

    /// <summary>Asynchronously saves all changes made in this context to the database.</summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>Asynchronously updates an entity in the database.</summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => databaseContext.Set<TEntity>().Update(entity), cancellationToken);
    }
}
