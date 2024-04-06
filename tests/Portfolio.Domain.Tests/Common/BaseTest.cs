using Bogus;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Factories;

namespace Portfolio.Domain.Tests.Common;

public abstract class BaseTest(PortfolioWebApplicationFactory factory) : IClassFixture<PortfolioWebApplicationFactory>
{
    protected readonly Faker _faker = new();
    protected readonly HttpClient _client = factory.CreateClient();
    protected readonly IAuthorRepository _authorRepository = factory.Services.GetRequiredService<IAuthorRepository>();
    protected readonly IUnitOfWork _unitOfWork = factory.Services.GetRequiredService<IUnitOfWork>();
    protected readonly IUserRepository _userRepository = factory.Services.GetRequiredService<IUserRepository>();
}
