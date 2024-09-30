using FluentValidation;
using FluentValidation.Validators;

using System.Text.RegularExpressions;

namespace AspNetTemplate.Infra.Common.Validators;

internal class PasswordValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => "PasswordValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        string passwordRegexValidator = @"^(?=.*[A-Z])(?=.*\d).+";

        return Regex.IsMatch(value?.ToString() ?? "", passwordRegexValidator);
    }
}
