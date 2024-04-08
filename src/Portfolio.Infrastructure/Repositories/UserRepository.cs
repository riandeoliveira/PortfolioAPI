using System.Linq.Expressions;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;
using Portfolio.Infrastructure.Repositories.Base;

namespace Portfolio.Infrastructure.Repositories;

public sealed class UserRepository(
    DatabaseContext databaseContext
) : BaseRepository<User>(databaseContext), IUserRepository
{
    public async Task<User> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User? user = await FindOneAsync(id, cancellationToken);

        return user is not null
            ? user
            : throw new BaseException(Message.UserNotFound);
    }

    public async Task<User> FindOneOrThrowAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        User? user = await FindOneAsync(predicate, cancellationToken);

        return user is not null
            ? user
            : throw new BaseException(Message.UserNotFound);
    }
}
