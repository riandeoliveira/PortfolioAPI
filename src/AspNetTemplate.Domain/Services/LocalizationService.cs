using System.Globalization;
using System.Reflection;

using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Messages;

namespace AspNetTemplate.Domain.Services;

public class LocalizationService
{
    public static string GetCultureName() => CultureInfo.CurrentCulture.Name;

    public static string? GetMessage(Message key)
    {
        return CultureInfo.CurrentUICulture.Name switch
        {
            "en-US" => GetMessageFromResource<Messages_EN_US>(key),
            "pt-BR" => GetMessageFromResource<Messages_PT_BR>(key),
            _ => null,
        };
    }

    private static string? GetMessageFromResource<T>(Message key)
    {
        FieldInfo? fieldInfo = typeof(T).GetField(key.ToString());

        return fieldInfo?.GetValue(null)?.ToString();
    }
}
