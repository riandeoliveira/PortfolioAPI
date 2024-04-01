using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace Portfolio.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task<TEntity?> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                entity => entity.Id == id && !entity.RemovedAt.HasValue,
                cancellationToken
            );

        return entity;
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>()
            .AsNoTracking()
            .Where(entity => !entity.RemovedAt.HasValue)
            .FirstOrDefaultAsync(predicate, cancellationToken);

        return entity;
    }
}
