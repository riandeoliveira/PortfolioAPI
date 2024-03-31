namespace Portfolio.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await databaseContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }
}
