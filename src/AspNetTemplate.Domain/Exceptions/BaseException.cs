using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Services;

namespace AspNetTemplate.Domain.Exceptions;

public sealed class BaseException : Exception
{
    public BaseException(Message key) : base(LocalizationService.GetMessage(key))
    {
    }

    public BaseException(string errorMessage) : base(errorMessage)
    {
    }
}
