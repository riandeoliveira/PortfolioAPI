using Microsoft.Extensions.Localization;

namespace AspNetTemplate.Interfaces;

public interface II18nService
{
    public LocalizedString T(string key, params object[] arguments);
}
