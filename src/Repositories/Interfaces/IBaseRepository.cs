using PortfolioAPI.Entities;

namespace PortfolioAPI.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity);

    Task DeleteAsync(T entity);

    Task<IEnumerable<T>> GetAsync();

    Task SaveChangesAsync();
}
