using FluentValidation;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Extensions;

namespace Portfolio.Application.UseCases.FindManyAuthors;

public sealed class FindManyAuthorsValidator : AbstractValidator<FindManyAuthorsRequest>
{
    public FindManyAuthorsValidator(ILocalizationService localizationService)
    {
        RuleFor(request => request.PageNumber)
            .LessThanOrEqualTo(100)
            .Message(localizationService, LocalizationMessages.MaximumPageNumberLength);

        RuleFor(request => request.PageSize)
            .LessThanOrEqualTo(100)
            .Message(localizationService, LocalizationMessages.MaximumPageSizeLength);
    }
}
