namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.Now;

        await Task.Run(() => context.Set<TEntity>().Update(entity), cancellationToken);
    }
}
