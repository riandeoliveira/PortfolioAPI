using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Entities;
using Portfolio.Entities.Context;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Repositories;

public class BaseRepository<TEntity>(ApplicationDbContext context) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ApplicationDbContext _context = context;

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entity;
    }

    public async Task<bool> ExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        return entity is not null;
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<TEntity?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public async Task Remove(TEntity? entity, CancellationToken cancellationToken = default)
    {
        await Task.Run(() =>
        {
            ArgumentNullException.ThrowIfNull(entity);

            _context.Set<TEntity>().Remove(entity);
        }, cancellationToken);

    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
