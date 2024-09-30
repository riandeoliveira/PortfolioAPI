using AspNetTemplate.Domain.Entities;
using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Data.Contexts;
using AspNetTemplate.Infra.Data.Exceptions;
using AspNetTemplate.Infra.Data.Interfaces;

using System.Linq.Expressions;

namespace AspNetTemplate.Infra.Data.Repositories;

public sealed class UserRepository(
    ApplicationDbContext context,
    IAuthService authService
) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User> FindAuthenticatedOrThrowAsync(CancellationToken cancellationToken = default)
    {
        Guid? userId = authService.FindAuthenticatedUserId();

        User user = await FindOneOrThrowAsync(userId, cancellationToken);

        return user;
    }

    public async Task<User> FindOneOrThrowAsync(Guid? id, CancellationToken cancellationToken = default)
    {

        Expression<Func<User, bool>> predicate = x => x.Id == id;

        return await FindOneOrThrowAsync(predicate, cancellationToken);
    }

    public async Task<User> FindOneOrThrowAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default)
    {
        User? user = await FindOneAsync(predicate, cancellationToken);

        return user is not null
            ? user
            : throw new NotFoundException(Message.UserNotFound);
    }
}
