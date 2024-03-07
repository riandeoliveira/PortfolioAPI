using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Users.Repositories;

public class UserRepository(DatabaseContext databaseContext) : BaseRepository<User>(databaseContext), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await FindAsync(user => user.Email == email, cancellationToken);
    }
}
