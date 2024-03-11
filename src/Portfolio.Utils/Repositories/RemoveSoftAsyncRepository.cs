using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
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
}
