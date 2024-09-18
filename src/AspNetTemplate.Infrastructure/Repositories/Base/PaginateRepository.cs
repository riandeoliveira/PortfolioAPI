using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Infrastructure.Tools;

namespace AspNetTemplate.Infrastructure.Repositories.Base;

public abstract partial class BaseRepository<TEntity>
{
    public async Task<PaginationDto<TEntity>> PaginateAsync(
        Guid id,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        Expression<Func<TEntity, bool>> predicate = entity => entity.Id == id && !entity.RemovedAt.HasValue;

        return await PaginateAsync(predicate, pageNumber, pageSize, cancellationToken);
    }

    public async Task<PaginationDto<TEntity>> PaginateAsync(
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> query = databaseContext.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate);

        return await PaginationTool<TEntity>.PaginateAsync(
            query,
            pageNumber,
            pageSize,
            cancellationToken
        );
    }
}
