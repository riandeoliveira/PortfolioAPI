using System.Linq.Expressions;

using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Exceptions;
using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Infrastructure.Contexts;
using AspNetTemplate.Infrastructure.Repositories.Base;

namespace AspNetTemplate.Infrastructure.Repositories;

public sealed class UserRepository(
    ApplicationDbContext context,
    IAuthService authService
) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User> FindAuthenticatedOrThrowAsync(CancellationToken cancellationToken = default)
    {
        Guid? userId = authService.FindAuthenticatedUserId() ?? throw new NotFoundException(Message.UserNotFound);

        User user = await FindOneOrThrowAsync(userId.Value, cancellationToken);

        return user;
    }

    public async Task<User> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        User? user = await FindOneAsync(id, cancellationToken);

        return user is not null
            ? user
            : throw new NotFoundException(Message.UserNotFound);
    }

    public async Task<User> FindOneOrThrowAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        User? user = await FindOneAsync(predicate, cancellationToken);

        return user is not null
            ? user
            : throw new NotFoundException(Message.UserNotFound);
    }
}
