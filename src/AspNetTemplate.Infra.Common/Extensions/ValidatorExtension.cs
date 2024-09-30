using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Common.Validators;
using AspNetTemplate.Infra.Data.Utilities;

using FluentValidation;

namespace AspNetTemplate.Infra.Common.Extensions;

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
        return ruleBuilder.WithMessage(LocalizationUtility.GetMessage(key));
    }

    public static IRuleBuilderOptions<T, TProperty> StrongPassword<T, TProperty>(
        this IRuleBuilder<T, TProperty> ruleBuilder
    )
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T, TProperty>());
    }
}
