using System.Linq.Expressions;

using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Domain.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default);

    Task<User> FindOneOrThrowAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken = default);
}
