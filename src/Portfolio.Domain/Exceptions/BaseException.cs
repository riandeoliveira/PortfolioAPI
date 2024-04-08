using Portfolio.Domain.Enums;
using Portfolio.Domain.Services;

namespace Portfolio.Domain.Exceptions;

public sealed class BaseException : Exception
{
    public BaseException(Message key) : base(LocalizationService.GetMessage(key))
    {
    }

    public BaseException(string errorMessage) : base(errorMessage)
    {
    }
}
