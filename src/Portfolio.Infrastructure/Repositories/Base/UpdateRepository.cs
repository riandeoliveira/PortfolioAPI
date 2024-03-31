namespace Portfolio.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.Now;

        await Task.Run(() => databaseContext.Set<TEntity>().Update(entity), cancellationToken);
    }
}
