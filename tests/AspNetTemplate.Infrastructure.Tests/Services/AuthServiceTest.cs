using AspNetTemplate.Domain.Tests.Common;
using AspNetTemplate.Domain.Tests.Factories;

using FluentAssertions;

using Microsoft.AspNetCore.Http;

namespace AspNetTemplate.Infrastructure.Tests.Services;

public sealed class AuthServiceTest(AspNetTemplateWebApplicationFactory factory) : BaseTest(factory)
{
    [Fact]
    public void ClearJwtCookies_Test()
    {
        HttpResponse? response = _httpContextAccessor?.HttpContext?.Response;

        response.Should().BeNull();
    }

    [Fact]
    public void CreateJwtTokenData_Test()
    {
    }

    [Fact]
    public void FindAuthenticatedUserId_Test()
    {
    }

    [Fact]
    public void GetJwtCookies_Test()
    {
    }

    [Fact]
    public void SendJwtCookiesToClient_Test()
    {
    }

    [Fact]
    public void ValidateJwtTokenOrThrow_Test()
    {
    }
}
