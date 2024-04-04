using FluentAssertions;

using Portfolio.Domain.Tests.Fixtures;

namespace Portfolio.Domain.Tests.Entities;

public sealed class BaseEntityTest
{
    [Fact]
    public static void ShouldCreateEntityThatInheritsFromBaseEntity()
    {
        FakeEntity entity = new();

        entity.Should().NotBeNull();
        entity.Id.Should().NotBe(Guid.Empty);
        entity.CreatedAt.Should().BeBefore(DateTime.Now);
        entity.RemovedAt.Should().BeNull();
        entity.UpdatedAt.Should().BeNull();
    }
}
