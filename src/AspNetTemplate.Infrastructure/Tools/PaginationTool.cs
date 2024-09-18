using Microsoft.EntityFrameworkCore;

using AspNetTemplate.Domain.Dtos;
using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Infrastructure.Tools;

public static class PaginationTool<TEntity> where TEntity : BaseEntity
{
    public static async Task<PaginationDto<TEntity>> PaginateAsync(
        IQueryable<TEntity> query,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken)
    {
        if (pageNumber <= 0 || pageSize <= 0)
        {
            pageNumber = 1;
            pageSize = 10;
        }

        int skipAmount = (pageNumber - 1) * pageSize;
        int totalItems = await query.CountAsync(cancellationToken);
        int totalPages = (int) Math.Ceiling((double) totalItems / pageSize);

        IEnumerable<TEntity> entities = await query
            .Skip(skipAmount)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginationDto<TEntity>(
            pageNumber,
            pageSize,
            totalPages,
            entities
        );
    }
}
