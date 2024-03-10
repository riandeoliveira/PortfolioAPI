using Portfolio.Utils.Enums;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Exceptions;

public sealed class BaseException : Exception
{
    public BaseException(ILocalizationService localizationService, LocalizationMessages key) : base(localizationService.GetKey(key))
    {
    }

    public BaseException(string errorMessage) : base(errorMessage)
    {
    }
}
