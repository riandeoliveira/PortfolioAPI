using System.Linq.Expressions;

using Portfolio.Authors.Interfaces;
using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Exceptions;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Authors.Repositories;

public class AuthorRepository(
    DatabaseContext databaseContext,
    IAuthService authService,
    ILocalizationService localizationService
) : BaseRepository<Author>(databaseContext), IAuthorRepository
{
    public async Task<Author> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        Author? author = await FindOneAsync(
            author => author.Id == id &&
            author.UserId == authService.GetLoggedInUserId(),
            cancellationToken
        );

        return author is not null
            ? author
            : throw new BaseException(localizationService, LocalizationMessages.AuthorNotFound);
    }

    public async Task<Author> FindOneOrThrowAsync(Expression<Func<Author, bool>> predicate, CancellationToken cancellationToken = default)
    {
        Expression<Func<Author, bool>> userIdPredicate = author
            => author.UserId == authService.GetLoggedInUserId();

        Expression<Func<Author, bool>> combinedPredicate = Expression.Lambda<Func<Author, bool>>(
            Expression.AndAlso(predicate.Body, userIdPredicate.Body),
            predicate.Parameters
        );

        Author? author = await FindOneAsync(predicate, cancellationToken);

        return author is not null
            ? author
            : throw new BaseException(localizationService, LocalizationMessages.AuthorNotFound);
    }
}
