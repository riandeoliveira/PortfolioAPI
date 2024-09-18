using AspNetTemplate.Domain.Entities;

namespace AspNetTemplate.Domain.Tests.Fixtures;

public static class DatabaseFixture
{
    public static User User_1 => new()
    {
        Id = Guid.Parse("46c19790-0e30-46e3-917c-2326316be6fc"),
        Email = "johndoe2000@email.com",
        Password = "LittleJohn2000",
        CreatedAt = new DateTime(2024, 4, 5, 14, 30, 0)
    };

    public static User User_2 => new()
    {
        Id = Guid.Parse("e1740729-fffb-4eb7-a40e-8094a2e6b053"),
        Email = "janesmith99@email.com",
        Password = "Password123",
        CreatedAt = new DateTime(2024, 4, 6, 10, 45, 0)
    };
}
