using System.Linq.Expressions;

using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Domain.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously creates a new entity in the database.</summary>
    /// <param name="entity">The entity to be created.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The created entity.</returns>
    Task<TEntity> CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously checks if an entity with the specified ID exists and is not removed.</summary>
    /// <param name="id">The ID of the entity to check.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the entity exists and is not removed, false otherwise.</returns>
    Task<bool> ExistAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously checks if an entity matching the specified predicate exists and is not removed.</summary>
    /// <param name="predicate">The predicate to match the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>True if the entity exists and is not removed, false otherwise.</returns>
    Task<bool> ExistAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously finds entities with the specified ID that are not removed.</summary>
    /// <param name="id">The ID of the entities to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of entities that match the specified ID and are not removed.</returns>
    Task<IEnumerable<TEntity>> FindManyAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously finds entities that match the specified predicate and are not removed.</summary>
    /// <param name="predicate">The predicate to match the entities.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A collection of entities that match the specified predicate and are not removed.</returns>
    Task<IEnumerable<TEntity>> FindManyAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously finds a single entity with the specified ID that is not removed.</summary>
    /// <param name="id">The ID of the entity to find.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entity that matches the specified ID and is not removed, or null if no such entity exists.</returns>
    Task<TEntity?> FindOneAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously finds a single entity that matches the specified predicate and is not removed.</summary>
    /// <param name="predicate">The predicate to match the entity.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The entity that matches the specified predicate and is not removed, or null if no such entity exists.</returns>
    Task<TEntity?> FindOneAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Asynchronously paginates a collection of entities based on the provided ID.
    /// </summary>
    /// <param name="id">The ID used for filtering the entities.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A PaginationDto containing the entities for the specified page.</returns>
    Task<PaginationDto<TEntity>> PaginateAsync(
        Guid id,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Asynchronously paginates a collection of entities based on the provided predicate.
    /// </summary>
    /// <param name="predicate">The predicate used for filtering the entities.</param>
    /// <param name="pageNumber">The page number to retrieve.</param>
    /// <param name="pageSize">The size of each page.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A PaginationDto containing the entities for the specified page.</returns>
    Task<PaginationDto<TEntity>> PaginateAsync(
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously removes an entity from the database permanently.</summary>
    /// <param name="entity">The entity to be removed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task RemoveHardAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously marks an entity as removed without deleting it from the database.</summary>
    /// <param name="entity">The entity to be marked as removed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task RemoveSoftAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );

    /// <summary>Asynchronously updates an entity in the database.</summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    Task UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken = default
    );
}
