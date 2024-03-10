using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Exceptions;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Repositories;

public class BaseRepository<TEntity>(
    DatabaseContext databaseContext,
    ILocalizationService localizationService
) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await databaseContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(
            entity => entity.Id == id &&
            !entity.ExcludedAt.HasValue,
            cancellationToken
        );

        return entity is not null;
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);

        return entity is not null && !entity.ExcludedAt.HasValue;
    }

    public async Task<TEntity> FindOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(
            entity => entity.Id == id &&
            !entity.ExcludedAt.HasValue,
            cancellationToken
        );

        return entity is not null
            ? entity
            : throw new BaseException(localizationService, LocalizationMessages.EntityNotFound);
    }

    public async Task<TEntity> FindOrThrowAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await databaseContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(predicate, cancellationToken);

        return entity is not null && !entity.ExcludedAt.HasValue
            ? entity
            : throw new BaseException(localizationService, LocalizationMessages.EntityNotFound);
    }

    public async Task RemoveHardAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => databaseContext.Set<TEntity>().Remove(entity), cancellationToken);
    }

    public async Task RemoveSoftAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            entity.ExcludedAt = DateTime.Now;

            databaseContext.Set<TEntity>().Update(entity);
        }, cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() => databaseContext.Set<TEntity>().Update(entity), cancellationToken);
    }
}
