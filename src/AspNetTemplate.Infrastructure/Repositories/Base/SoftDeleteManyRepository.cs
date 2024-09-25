using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task SoftDeleteManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);

        foreach (TEntity entity in entities)
        {
            entity.DeletedAt = DateTime.Now;
        }

        context.Set<TEntity>().UpdateRange(entities);
    }
}
