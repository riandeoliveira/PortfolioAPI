using Microsoft.Extensions.Localization;

using Portfolio.Utils.Enums;

namespace Portfolio.Utils.Interfaces;

public interface ILocalizationService
{
    LocalizedString GetKey(LocalizationMessages key);
}
