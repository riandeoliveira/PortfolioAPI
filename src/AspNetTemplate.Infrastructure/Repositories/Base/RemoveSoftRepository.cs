namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task RemoveSoftAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            entity.RemovedAt = DateTime.Now;

            databaseContext.Set<TEntity>().Update(entity);
        }, cancellationToken);
    }
}
