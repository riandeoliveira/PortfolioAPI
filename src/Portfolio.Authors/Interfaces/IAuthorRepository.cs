using System.Linq.Expressions;

using Portfolio.Domain.Entities;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Authors.Interfaces;

public interface IAuthorRepository : IBaseRepository<Author>
{
    Task<Author> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Author> FindOneOrThrowAsync(Expression<Func<Author, bool>> predicate, CancellationToken cancellationToken = default);
}
