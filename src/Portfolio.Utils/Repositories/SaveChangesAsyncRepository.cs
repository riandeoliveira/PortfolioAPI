using Portfolio.Domain.Entities;

namespace Portfolio.Utils.Repositories;

public abstract partial class BaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>Asynchronously saves all changes made in this context to the database.</summary>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
