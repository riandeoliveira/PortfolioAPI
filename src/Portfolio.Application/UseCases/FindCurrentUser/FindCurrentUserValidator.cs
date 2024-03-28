using FluentValidation;

namespace Portfolio.Application.UseCases.FindCurrentUser;

public sealed class FindCurrentUserValidator : AbstractValidator<FindCurrentUserRequest>
{
    public FindCurrentUserValidator()
    {
    }
}
