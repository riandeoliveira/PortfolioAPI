using FluentValidation;

using Portfolio.Utils.Validators;

namespace Portfolio.Utils.Extensions;

public static class ValidatorExtension
{
    public static IRuleBuilderOptions<T, TProperty> EmailAddress<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new EmailValidator<T, TProperty>());
    }

    public static IRuleBuilderOptions<T, TProperty> StrongPassword<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new PasswordValidator<T, TProperty>());
    }
}
