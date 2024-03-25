using FluentValidation;

namespace Portfolio.Application.UseCases.RemoveUser;

public sealed class RemoveUserValidator : AbstractValidator<RemoveUserRequest>
{
    public RemoveUserValidator()
    {
    }
}
