using Bogus;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Domain.Tests.Common;

public abstract class BaseIntegrationTest(IntegrationTestWebAppFactory factory) : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly DatabaseContext _context = factory.Services.GetRequiredService<DatabaseContext>();
    protected readonly Faker _faker = new();
    protected readonly HttpClient _client = factory.CreateClient();
    protected readonly IAuthorRepository _authorRepository = factory.Services.GetRequiredService<IAuthorRepository>();
    protected readonly IAuthService _authService = factory.Services.GetRequiredService<IAuthService>();
    protected readonly IHttpContextAccessor _accessor = factory.Services.GetRequiredService<IHttpContextAccessor>();
    protected readonly ILocalizationService _localizationService = factory.Services.GetRequiredService<ILocalizationService>();
    protected readonly IUnitOfWork _unitOfWork = factory.Services.GetRequiredService<IUnitOfWork>();
    protected readonly IUserRepository _userRepository = factory.Services.GetRequiredService<IUserRepository>();
}
