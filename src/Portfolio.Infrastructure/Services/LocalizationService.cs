using System.Reflection;

using Microsoft.Extensions.Localization;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Infrastructure.Services;

public sealed record LocalizationResource;

public sealed class LocalizationService : ILocalizationService
{
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        string? assemblyFullName = typeof(LocalizationResource).GetTypeInfo().Assembly.FullName;
        AssemblyName assemblyName = new(assemblyFullName ?? "");

        _localizer = factory.Create("LocalizationResource", assemblyName.Name ?? "");
    }

    public LocalizedString GetKey(LocalizationMessages key)
    {
        return _localizer[key.ToString()];
    }
}
