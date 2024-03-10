using FluentValidation;
using FluentValidation.Results;

using Portfolio.Utils.Enums;
using Portfolio.Utils.Interfaces;
using Portfolio.Utils.Validators;

namespace Portfolio.Utils.Extensions;

public static class ValidatorExtension
{
    public static IRuleBuilderOptions<T, TProperty> EmailAddress<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
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

    public static IRuleBuilderOptions<T, TProperty> StrongPassword<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T, TProperty>());
    }

    public static async Task ValidateRequestAsync<TRequest>(
        this AbstractValidator<TRequest> validator,
        TRequest request,
        CancellationToken cancellationToken = default
    )
    {
        ValidationResult result = await validator.ValidateAsync(request, cancellationToken);

        if (!result.IsValid)
        {
            throw new ValidationException(result.Errors.First().ErrorMessage);
        }
    }
}
