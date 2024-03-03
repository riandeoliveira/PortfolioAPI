using System.Reflection;

using Microsoft.Extensions.Localization;

using Portfolio.Utils.Enums;
using Portfolio.Utils.Interfaces;

namespace Portfolio.Utils.Services;

public sealed record LocalizationResource;

public sealed class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        var type = typeof(LocalizationResource);
        var assemblyFullName = type.GetTypeInfo().Assembly.FullName;
        var assemblyName = new AssemblyName(assemblyFullName ?? "");

        _localizer = factory.Create("LocalizationResource", assemblyName.Name ?? "");
    }

    public LocalizedString GetKey(LocalizationKeys key)
    {
        return _localizer[key.ToString()];
    }
}
