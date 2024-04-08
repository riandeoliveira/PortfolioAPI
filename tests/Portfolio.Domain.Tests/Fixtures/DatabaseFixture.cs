using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Tests.Fixtures;

public static class DatabaseFixture
{
    public static Author Author => new()
    {
        Id = Guid.Parse("b4b5a854-7177-481e-b47a-41414a82ae4d"),
        Name = "John",
        FullName = "John Doe",
        Position = "Full Stack Developer",
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        AvatarUrl = "https://johndoe.com/avatar.png",
        SpotifyAccountName = "johndoe2000",
        UserId = User.Id,
        CreatedAt = new DateTime(2024, 4, 5, 14, 30, 0)
    };

    public static User User => new()
    {
        Id = Guid.Parse("46c19790-0e30-46e3-917c-2326316be6fc"),
        Email = "johndoe2000@email.com",
        Password = "LittleJohn2000",
        CreatedAt = new DateTime(2024, 4, 5, 14, 30, 0)
    };
}
