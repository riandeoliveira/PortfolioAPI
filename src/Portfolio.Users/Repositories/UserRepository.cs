using FluentValidation;

using Portfolio.Domain.Context;
using Portfolio.Domain.Entities;
using Portfolio.Users.Interfaces;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Repositories;

namespace Portfolio.Users.Repositories;

public class UserRepository(
    DatabaseContext databaseContext,
    ILocalizationService localizationService
) : BaseRepository<User>(databaseContext), IUserRepository
{
    private readonly ILocalizationService _localizationService = localizationService;

    public async Task<User> FindByEmailOrThrowAsync(string email, CancellationToken cancellationToken)
    {
        User? user = await FindAsync(user => user.Email == email, cancellationToken);

        if (user is not null) return user;

        throw new ValidationException(_localizationService.GetKey(LocalizationMessages.UserNotFound));
    }
}
