using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;

using Portfolio.Entities;
using Portfolio.Entities.Context;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Repositories;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;

    public async Task<T> CreateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _context.Set<T>().AddAsync(entity);

        return entity;
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).FirstOrDefaultAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await Task.Run(() => _context.Set<T>().Remove(entity));
    }

    public async Task<IEnumerable<T>> GetAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
