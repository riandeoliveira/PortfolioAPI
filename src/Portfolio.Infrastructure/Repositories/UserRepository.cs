using System.Linq.Expressions;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

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
