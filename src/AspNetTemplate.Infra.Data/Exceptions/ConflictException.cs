using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Infra.Data.Utilities;

namespace AspNetTemplate.Infra.Data.Exceptions;

public sealed class ConflictException(Message key) : Exception(LocalizationUtility.GetMessage(key))
{
}
