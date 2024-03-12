using System.Linq.Expressions;

using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Users.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User> FindOneOrThrowAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);
}
