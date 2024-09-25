using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task<IEnumerable<TEntity>> FindManyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await context.Set<TEntity>()
            .AsNoTracking()
            .Where(entity => entity.Id == id && !entity.DeletedAt.HasValue)
            .ToListAsync(cancellationToken);

        return entities;
    }

    public async Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await context.Set<TEntity>()
            .AsNoTracking()
            .Where(entity => !entity.DeletedAt.HasValue)
            .Where(predicate)
            .ToListAsync(cancellationToken);

        return entities;
    }
}
