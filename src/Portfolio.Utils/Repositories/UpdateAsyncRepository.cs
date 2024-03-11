using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously updates an entity in the database.</summary>
    /// <param name="entity">The entity to be updated.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => databaseContext.Set<TEntity>().Update(entity), cancellationToken);
    }
}
