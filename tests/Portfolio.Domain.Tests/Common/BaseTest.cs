using Bogus;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Domain.Interfaces;
using Portfolio.Domain.Tests.Factories;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Domain.Tests.Common;

public abstract class BaseTest : IClassFixture<PortfolioWebApplicationFactory>
{
    private readonly PortfolioWebApplicationFactory _factory;

    protected readonly DatabaseContext _databaseContext;
    protected readonly Faker _faker = new();
    protected readonly HttpClient _client;
    protected readonly IAuthorRepository _authorRepository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IUserRepository _userRepository;

    public BaseTest(PortfolioWebApplicationFactory factory)
    {
        _factory = factory;

        _databaseContext = GetService<DatabaseContext>();
        _faker = new();
        _client = factory.CreateClient();
        _authorRepository = GetService<IAuthorRepository>();
        _userRepository = GetService<IUserRepository>();
        _unitOfWork = GetService<IUnitOfWork>();
    }

    private TService GetService<TService>() where TService : notnull
    {
        return _factory.Services.GetRequiredService<TService>();
    }
}
