using FluentValidation;

namespace AspNetTemplate.Application.UseCases.RemoveUser;

public sealed class RemoveUserValidator : AbstractValidator<RemoveUserRequest>
{
    public RemoveUserValidator()
    {
    }
}
