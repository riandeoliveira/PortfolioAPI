using Bogus;

using Microsoft.Extensions.DependencyInjection;

using AspNetTemplate.Domain.Interfaces;
using AspNetTemplate.Domain.Tests.Factories;
using AspNetTemplate.Infrastructure.Contexts;

namespace AspNetTemplate.Domain.Tests.Common;

public abstract class BaseTest : IClassFixture<AspNetTemplateWebApplicationFactory>
{
    private readonly AspNetTemplateWebApplicationFactory _factory;

    protected readonly DatabaseContext _databaseContext;
    protected readonly Faker _faker = new();
    protected readonly HttpClient _client;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IUserRepository _userRepository;

    public BaseTest(AspNetTemplateWebApplicationFactory factory)
    {
        _factory = factory;

        _databaseContext = GetService<DatabaseContext>();
        _faker = new();
        _client = factory.CreateClient();
        _userRepository = GetService<IUserRepository>();
        _unitOfWork = GetService<IUnitOfWork>();
    }

    private TService GetService<TService>() where TService : notnull
    {
        return _factory.Services.GetRequiredService<TService>();
    }
}
