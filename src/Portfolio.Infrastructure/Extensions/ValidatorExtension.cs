using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Services;
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
        Message key
    )
    {
        return ruleBuilder.WithMessage(LocalizationService.GetMessage(key));
    }

    public static IRuleBuilderOptions<T, TProperty> StrongPassword<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder
    )
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T, TProperty>());
    }
}
