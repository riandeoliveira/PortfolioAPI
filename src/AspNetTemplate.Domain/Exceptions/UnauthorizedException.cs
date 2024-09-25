using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Services;

namespace AspNetTemplate.Domain.Exceptions;

public sealed class UnauthorizedException(Message key) : Exception(LocalizationService.GetMessage(key))
{
}
