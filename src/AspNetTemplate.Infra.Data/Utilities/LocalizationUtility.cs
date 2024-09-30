using AspNetTemplate.Domain.Enums;
using AspNetTemplate.Domain.Messages;

using System.Globalization;
using System.Reflection;

namespace AspNetTemplate.Infra.Data.Utilities;

public class LocalizationUtility
{
    private static string? GetMessageFromResource<T>(Message key)
    {
        FieldInfo? fieldInfo = typeof(T).GetField(key.ToString());

        return fieldInfo?.GetValue(null)?.ToString();
    }

    public static string? GetCultureName() => CultureInfo.CurrentUICulture.Name;

    public static string? GetMessage(Message key)
    {
        string? cultureName = GetCultureName();

        return cultureName switch
        {
            "en-US" => GetMessageFromResource<Messages_EN_US>(key),
            "pt-BR" => GetMessageFromResource<Messages_PT_BR>(key),
            _ => null,
        };
    }

}
