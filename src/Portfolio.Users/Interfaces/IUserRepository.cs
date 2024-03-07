using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Users.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);
}
