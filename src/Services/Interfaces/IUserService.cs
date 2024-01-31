using PortfolioAPI.Entities;
using PortfolioAPI.Services.Dtos;

namespace PortfolioAPI.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateAsync(CreateUserDTO dto);

    Task DeleteAsync(Guid id);

    Task<IEnumerable<User>> GetAsync();
}
