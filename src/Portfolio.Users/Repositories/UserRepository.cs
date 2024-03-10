using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Users.Repositories;

public class UserRepository(
    DatabaseContext databaseContext,
    ILocalizationService localizationService
) : BaseRepository<User>(databaseContext, localizationService), IUserRepository
{
}
