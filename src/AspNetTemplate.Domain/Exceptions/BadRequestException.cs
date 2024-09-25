using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Services;

namespace AspNetTemplate.Domain.Exceptions;

public sealed class BadRequestException(Message key) : Exception(LocalizationService.GetMessage(key))
{
}
