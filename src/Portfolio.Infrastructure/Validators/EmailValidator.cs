using System.Text.RegularExpressions;

using FluentValidation;
using FluentValidation.Validators;

namespace Portfolio.Infrastructure.Validators;

internal class EmailValidator<T, TProperty> : PropertyValidator<T, TProperty>
{
    public override string Name => "EmailValidator";

    public override bool IsValid(ValidationContext<T> context, TProperty value)
    {
        string emailRegexPattern = @"^[A-Za-z0-9. _%-]+@[A-Za-z0-9. -]+\.[A-Za-z]{2,4}$";

        return Regex.IsMatch(value?.ToString() ?? "", emailRegexPattern);
    }
}
