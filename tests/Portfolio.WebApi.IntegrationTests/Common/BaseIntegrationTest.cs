using Bogus;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;

using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces;
using Portfolio.Infrastructure.Contexts;
using Portfolio.WebApi.IntegrationTests.Factories;

namespace Portfolio.WebApi.IntegrationTests.Common;

public abstract class BaseIntegrationTest(IntegrationTestWebAppFactory factory) : IClassFixture<IntegrationTestWebAppFactory>
{
    protected readonly DatabaseContext _context = factory.Services.GetRequiredService<DatabaseContext>();
    protected readonly Faker _faker = new();
    protected readonly HttpClient _client = factory.CreateClient();
    protected readonly IAuthorRepository _authorRepository = factory.Services.GetRequiredService<IAuthorRepository>();
    protected readonly IAuthService _authService = factory.Services.GetRequiredService<IAuthService>();
    protected readonly IUnitOfWork _unitOfWork = factory.Services.GetRequiredService<IUnitOfWork>();
    protected readonly IUserRepository _userRepository = factory.Services.GetRequiredService<IUserRepository>();

    protected static void ExecuteEntityTests<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        entity.Should().NotBeNull();
        entity.Id.Should().NotBe(Guid.Empty);
        entity.CreatedAt.Should().BeBefore(DateTime.Now);
        entity.RemovedAt.Should().BeNull();
        entity.UpdatedAt.Should().BeNull();
    }
}
