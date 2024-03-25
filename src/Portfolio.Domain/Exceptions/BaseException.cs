using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Domain.Exceptions;

public sealed class BaseException : Exception
{
    public BaseException(ILocalizationService localizationService, LocalizationMessages key) : base(localizationService.GetKey(key))
    {
    }

    public BaseException(string errorMessage) : base(errorMessage)
    {
    }
}
