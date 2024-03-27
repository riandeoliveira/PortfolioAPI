using Microsoft.Extensions.Localization;

using Portfolio.Domain.Enums;

namespace Portfolio.Domain.Interfaces;

public interface ILocalizationService
{
    string? GetCultureName();

    LocalizedString GetKey(LocalizationMessages key);
}
