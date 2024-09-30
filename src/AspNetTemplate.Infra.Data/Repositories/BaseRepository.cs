using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Infra.Data.Contexts;
using AspNetTemplate.Infra.Data.Dtos;
using AspNetTemplate.Infra.Data.Interfaces;
using AspNetTemplate.Infra.Data.Utilities;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace AspNetTemplate.Infra.Data.Repositories;

public abstract class BaseRepository<TEntity>(
    ApplicationDbContext context
) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await context.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Expression<Func<TEntity, bool>> predicate = x => x.Id == id;

        return await ExistAsync(predicate, cancellationToken);
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);

        return entity is not null;
    }

    public async Task<IEnumerable<TEntity>> FindManyAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Expression<Func<TEntity, bool>> predicate = x => x.Id == id;

        return await FindManyAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> FindManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);

        return entities;
    }

    public async Task<TEntity?> FindOneAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Expression<Func<TEntity, bool>> predicate = x => x.Id == id;

        return await FindOneAsync(predicate, cancellationToken);
    }

    public async Task<TEntity?> FindOneAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(predicate, cancellationToken);

        return entity;
    }

    public async Task HardDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => context.Set<TEntity>().Remove(entity), cancellationToken);
    }

    public async Task<PaginationDto<TEntity>> PaginateAsync(
        Guid id,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        Expression<Func<TEntity, bool>> predicate = x => x.Id == id;

        return await PaginateAsync(predicate, pageNumber, pageSize, cancellationToken);
    }

    public async Task<PaginationDto<TEntity>> PaginateAsync(
        Expression<Func<TEntity, bool>> predicate,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<TEntity> query = context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate);

        return await PaginationUtility<TEntity>.PaginateAsync(
            query,
            pageNumber,
            pageSize,
            cancellationToken
        );
    }

    public async Task SoftDeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            entity.DeletedAt = DateTime.UtcNow;

            context.Set<TEntity>().Update(entity);
        }, cancellationToken);
    }

    public async Task SoftDeleteManyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        IEnumerable<TEntity> entities = await context.Set<TEntity>()
            .AsNoTracking()
            .Where(predicate)
            .ToListAsync(cancellationToken);

        foreach (TEntity entity in entities)
        {
            entity.DeletedAt = DateTime.UtcNow;
        }

        context.Set<TEntity>().UpdateRange(entities);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.UpdatedAt = DateTime.UtcNow;

        await Task.Run(() => context.Set<TEntity>().Update(entity), cancellationToken);
    }
}
