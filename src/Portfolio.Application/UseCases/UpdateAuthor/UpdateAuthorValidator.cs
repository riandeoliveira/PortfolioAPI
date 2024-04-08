using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.UpdateAuthor;

public sealed class UpdateAuthorValidator : AbstractValidator<UpdateAuthorRequest>
{
    public UpdateAuthorValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .Message(Message.NameIsRequired)

            .MaximumLength(64)
            .Message(Message.MaximumNameLength);

        RuleFor(request => request.FullName)
            .NotEmpty()
            .Message(Message.FullNameIsRequired)

            .MaximumLength(128)
            .Message(Message.MaximumFullNameLength);

        RuleFor(request => request.Position)
            .NotEmpty()
            .Message(Message.PositionIsRequired)

            .MaximumLength(64)
            .Message(Message.MaximumPositionLength);

        RuleFor(request => request.Description)
            .NotEmpty()
            .Message(Message.DescriptionIsRequired)

            .MaximumLength(1024)
            .Message(Message.MaximumPositionLength);

        RuleFor(request => request.AvatarUrl)
            .NotEmpty()
            .Message(Message.AvatarUrlIsRequired)

            .MaximumLength(512)
            .Message(Message.MaximumAvatarUrlLength);

        RuleFor(request => request.SpotifyAccountName)
            .MaximumLength(64)
            .Message(Message.MaximumSpotifyAccountNameLength);
    }
}
