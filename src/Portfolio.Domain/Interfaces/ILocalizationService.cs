using Portfolio.Domain.Enums;

using Microsoft.Extensions.Localization;

namespace Portfolio.Domain.Interfaces;

public interface ILocalizationService
{
    string? GetCultureName();

    LocalizedString GetKey(LocalizationMessages key);
}
