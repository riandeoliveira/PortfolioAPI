using FluentValidation;
using FluentValidation.Results;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Exceptions;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Validators;

namespace Portfolio.Infrastructure.Extensions;

public static class ValidatorExtension
{
    public static IRuleBuilderOptions<T, TProperty> EmailAddress<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder
    )
    {
        return ruleBuilder.SetValidator(new EmailValidator<T, TProperty>());
    }

    public static IRuleBuilderOptions<T, TProperty> Message<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> ruleBuilder,
        ILocalizationService localizationService,
        LocalizationMessages key
    )
    {
        return ruleBuilder.WithMessage(localizationService.GetKey(key));
    }

    public static IRuleBuilderOptions<T, TProperty> StrongPassword<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder
    )
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T, TProperty>());
    }

    public static async Task ValidateOrThrowAsync<TRequest>(
        this AbstractValidator<TRequest> validator,
        TRequest request,
        CancellationToken cancellationToken = default
    )
    {
        ValidationResult result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid) throw new BaseException(result.Errors.First().ErrorMessage);
    }
}
