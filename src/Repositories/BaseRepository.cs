using Microsoft.EntityFrameworkCore;

using PortfolioAPI.Context;
using PortfolioAPI.Entities;
using PortfolioAPI.Repositories.Interfaces;

namespace PortfolioAPI.Repositories;

public class BaseRepository<T>(ApplicationDbContext context) : IBaseRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context = context;

    public async Task<T> CreateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _context.Set<T>().AddAsync(entity);

        return entity;
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
