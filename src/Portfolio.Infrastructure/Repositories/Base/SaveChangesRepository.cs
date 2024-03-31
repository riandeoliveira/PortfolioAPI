namespace Portfolio.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
