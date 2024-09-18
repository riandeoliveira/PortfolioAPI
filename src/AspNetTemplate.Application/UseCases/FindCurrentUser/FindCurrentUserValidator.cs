using FluentValidation;

namespace AspNetTemplate.Application.UseCases.FindCurrentUser;

public sealed class FindCurrentUserValidator : AbstractValidator<FindCurrentUserRequest>
{
    public FindCurrentUserValidator()
    {
    }
}
