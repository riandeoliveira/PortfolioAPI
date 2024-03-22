using System.Linq.Expressions;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

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
