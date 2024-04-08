using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed class FindManyAuthorsValidator : AbstractValidator<FindManyAuthorsRequest>
{
    public FindManyAuthorsValidator()
    {
        RuleFor(request => request.PageNumber)
            .LessThanOrEqualTo(100)
            .Message(Message.MaximumPageNumberLength);

        RuleFor(request => request.PageSize)
            .LessThanOrEqualTo(100)
            .Message(Message.MaximumPageSizeLength);
    }
}
