using System.Linq.Expressions;

using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Exceptions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Users.Repositories;

public class UserRepository(
    DatabaseContext databaseContext,
    ILocalizationService localizationService
) : BaseRepository<User>(databaseContext), IUserRepository
{
    public async Task<User> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User? user = await FindOneAsync(id, cancellationToken);

        return user is not null
            ? user
            : throw new BaseException(localizationService, LocalizationMessages.UserNotFound);
    }

    public async Task<User> FindOneOrThrowAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        User? user = await FindOneAsync(predicate, cancellationToken);

        return user is not null
            ? user
            : throw new BaseException(localizationService, LocalizationMessages.UserNotFound);
    }
}
