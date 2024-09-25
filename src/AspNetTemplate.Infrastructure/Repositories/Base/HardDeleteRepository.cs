namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => context.Set<TEntity>().Remove(entity), cancellationToken);
    }
}
