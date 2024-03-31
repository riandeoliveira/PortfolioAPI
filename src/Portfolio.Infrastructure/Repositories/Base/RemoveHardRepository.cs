namespace Portfolio.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task RemoveHardAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => databaseContext.Set<TEntity>().Remove(entity), cancellationToken);
    }
}
