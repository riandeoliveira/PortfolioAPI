using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Entities;
using Portfolio.Entities.Context;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Repositories;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _context.Set<T>().AddAsync(entity, cancellationToken);

        return entity;
    }

    // NOTE: IMPLEMENT
    // public async Task ExistAsync(Guid id, CancellationToken cancellationToken = default)
    // {

    // }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<T?> FindAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }

    public void Remove(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _context.Set<T>().Remove(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
