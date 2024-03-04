using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Context;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Repositories;

public class BaseRepository<TEntity>(DatabaseContext databaseContext) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DatabaseContext _databaseContext = databaseContext;

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _databaseContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _databaseContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        return entity is not null;
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await _databaseContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);

        return entity is not null;
    }

    public async Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _databaseContext.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _databaseContext.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task Remove(TEntity? entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            ArgumentNullException.ThrowIfNull(entity);

            _databaseContext.Set<TEntity>().Remove(entity);
        }, cancellationToken);

    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}
