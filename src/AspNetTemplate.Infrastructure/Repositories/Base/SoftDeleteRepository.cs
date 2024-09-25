namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            entity.DeletedAt = DateTime.Now;

            context.Set<TEntity>().Update(entity);
        }, cancellationToken);
    }
}
