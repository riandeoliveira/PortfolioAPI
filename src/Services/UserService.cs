using PortfolioAPI.Entities;
using PortfolioAPI.Repositories.Interfaces;
using PortfolioAPI.Services.Dtos;
using PortfolioAPI.Services.Interfaces;

namespace PortfolioAPI.Services;

public sealed class UserService(IBaseRepository<User> repository) : IUserService
{
    private readonly IBaseRepository<User> _repository = repository;

    public async Task<User> CreateAsync(CreateUserDTO dto)
    {
        var user = new User
        {
            Name = dto.Name,
            FullName = dto.FullName,
            Position = dto.Position,
            Description = dto.Description,
            AvatarUrl = dto.AvatarUrl,
            SpotifyAccountName = dto.SpotifyAccountName
        };

        var createdUser = await _repository.CreateAsync(user);

        await _repository.SaveChangesAsync();

        return createdUser;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _repository.GetAsync();
        var userToDelete = user.Where(x => x.Id == id).First();

        await _repository.DeleteAsync(userToDelete);
        await _repository.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAsync()
    {
        return await _repository.GetAsync();
    }
}
