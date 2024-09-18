using System.Linq.Expressions;

using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Contexts;
using AspNetTemplate.Infrastructure.Repositories.Base;

namespace AspNetTemplate.Infrastructure.Repositories;

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
