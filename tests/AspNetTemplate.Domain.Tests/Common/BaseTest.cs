using Bogus;

using Microsoft.Extensions.DependencyInjection;

using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Domain.Tests.Factories;
using AspNetTemplate.Infrastructure.Contexts;
using System.Text.RegularExpressions;
using FluentAssertions;
using System.Security.Claims;

namespace AspNetTemplate.Domain.Tests.Common;

public abstract class BaseTest : IClassFixture<AspNetTemplateWebApplicationFactory>
{
    private readonly AspNetTemplateWebApplicationFactory _factory;

    protected readonly ApplicationDbContext _context;
    protected readonly Faker _faker = new();
    protected readonly HttpClient _client;
    protected readonly IAuthService _authService;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IUserRepository _userRepository;

    public BaseTest(AspNetTemplateWebApplicationFactory factory)
    {
        _factory = factory;

        _context = GetService<ApplicationDbContext>();
        _faker = new();
        _client = factory.CreateClient();
        _authService = GetService<IAuthService>();
        _userRepository = GetService<IUserRepository>();
        _unitOfWork = GetService<IUnitOfWork>();
    }

    private TService GetService<TService>() where TService : notnull
    {
        return _factory.Services.GetRequiredService<TService>();
    }

    public void ShouldHaveValidJwtCookies(IEnumerable<string> cookies)
    {
        Match accessTokenMatch = Regex.Match(cookies.ElementAt(0), $@"access_token=([^;]+)");
        Match refreshTokenMatch = Regex.Match(cookies.ElementAt(1), $@"refresh_token=([^;]+)");

        bool hasJwtCookies = accessTokenMatch.Success && refreshTokenMatch.Success;

        string accessToken = accessTokenMatch.Groups[1].Value;
        string refreshToken = refreshTokenMatch.Groups[1].Value;

        hasJwtCookies.Should().BeTrue();

        ClaimsPrincipal accessTokenPrincipal = _authService.ValidateJwtTokenOrThrow(accessToken);
        ClaimsPrincipal refreshTokenPrincipal = _authService.ValidateJwtTokenOrThrow(refreshToken);

        accessTokenPrincipal.Should().BeOfType<ClaimsPrincipal>();
        refreshTokenPrincipal.Should().BeOfType<ClaimsPrincipal>();
    }
}
