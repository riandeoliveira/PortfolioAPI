using System.Linq.Expressions;

using Portfolio.Domain.Dtos;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;
using Portfolio.Infrastructure.Repositories.Base;

namespace Portfolio.Infrastructure.Repositories;

public sealed class AuthorRepository(
    DatabaseContext databaseContext,
    IAuthService authService,
    ILocalizationService localizationService
) : BaseRepository<Author>(databaseContext), IAuthorRepository
{
    public async Task<Author> FindOneOrThrowAsync(Guid id, CancellationToken cancellationToken = default)
    {
        UserDto userDto = await authService.GetCurrentUserAsync(cancellationToken);

        Author? author = await FindOneAsync(
            author => author.Id == id &&
            author.UserId == userDto.Id,
            cancellationToken
        );

        return author is not null
            ? author
            : throw new BaseException(localizationService, LocalizationMessages.AuthorNotFound);
    }

    public async Task<Author> FindOneOrThrowAsync(Expression<Func<Author, bool>> predicate, CancellationToken cancellationToken = default)
    {
        UserDto userDto = await authService.GetCurrentUserAsync(cancellationToken);

        Expression<Func<Author, bool>> userIdPredicate = author => author.UserId == userDto.Id;

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
