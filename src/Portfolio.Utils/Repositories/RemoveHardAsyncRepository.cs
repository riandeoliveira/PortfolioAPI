using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously removes an entity from the database permanently.</summary>
    /// <param name="entity">The entity to be removed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task RemoveHardAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => databaseContext.Set<TEntity>().Remove(entity), cancellationToken);
    }
}
