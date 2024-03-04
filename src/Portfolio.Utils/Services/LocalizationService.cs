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
        Type type = typeof(LocalizationResource);
        string? assemblyFullName = type.GetTypeInfo().Assembly.FullName;
        AssemblyName assemblyName = new(assemblyFullName ?? "");

        _localizer = factory.Create("LocalizationResource", assemblyName.Name ?? "");
    }

    public LocalizedString GetKey(LocalizationMessages key)
    {
        return _localizer[key.ToString()];
    }
}
