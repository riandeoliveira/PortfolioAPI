namespace Portfolio.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task RemoveSoftAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            entity.ExcludedAt = DateTime.Now;

            databaseContext.Set<TEntity>().Update(entity);
        }, cancellationToken);
    }
}
