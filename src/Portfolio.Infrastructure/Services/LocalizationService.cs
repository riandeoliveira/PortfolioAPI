using System.Reflection;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

using Portfolio.Domain.Enums;
using Portfolio.Domain.Interfaces;

namespace Portfolio.Infrastructure.Services;

public sealed record LocalizationResource;

public sealed class LocalizationService : ILocalizationService
{
    private readonly IHttpContextAccessor _accessor;
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IHttpContextAccessor accessor, IStringLocalizerFactory factory)
    {
        _accessor = accessor;

        string? assemblyFullName = typeof(LocalizationResource).GetTypeInfo().Assembly.FullName;
        AssemblyName assemblyName = new(assemblyFullName ?? "");

        _localizer = factory.Create("LocalizationResource", assemblyName.Name ?? "");
    }

    public string? GetCultureName()
    {
        return _accessor.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name;
    }

    public LocalizedString GetKey(LocalizationMessages key)
    {
        return _localizer[key.ToString()];
    }
}
