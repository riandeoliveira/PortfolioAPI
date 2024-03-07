using FluentValidation;

using Portfolio.Users.Interfaces;
using Portfolio.Users.Requests;
using Portfolio.Utils.Enums;
using Portfolio.Utils.Extensions;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Users.Validators;

public sealed class SignInUserValidator : AbstractValidator<SignInUserRequest>
{
    private readonly ILocalizationService _localizationService;
    private readonly IUserRepository _userRepository;

    public SignInUserValidator(ILocalizationService localizationService, IUserRepository userRepository)
    {
        _localizationService = localizationService;
        _userRepository = userRepository;

        RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.EmailIsRequired))

            .MinimumLength(8)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MinimumEmailLength))

            .MaximumLength(64)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumEmailLength))

            .EmailAddress()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.InvalidEmail))

            .MustAsync(EmailMustBeRegistered)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.EmailIsNotRegistered));

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.PasswordIsRequired))

            .MinimumLength(8)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MinimumPasswordLength))

            .MaximumLength(64)
            .WithMessage(_localizationService.GetKey(LocalizationMessages.MaximumPasswordLength))

            .StrongPassword()
            .WithMessage(_localizationService.GetKey(LocalizationMessages.StrongPassword));
    }

    private async Task<bool> EmailMustBeRegistered(string email, CancellationToken cancellationToken = default)
    {
        return await _userRepository.ExistAsync(user => user.Email == email, cancellationToken);
    }
}
