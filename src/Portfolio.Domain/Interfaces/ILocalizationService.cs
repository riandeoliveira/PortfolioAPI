using Portfolio.Domain.Enums;

using Microsoft.Extensions.Localization;

namespace Portfolio.Domain.Interfaces;

public interface ILocalizationService
{
    LocalizedString GetKey(LocalizationMessages key);
}
