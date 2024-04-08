using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Tests.Fixtures;

public static class DatabaseFixture
{
    public static Author Author_1 => new()
    {
        Id = Guid.Parse("b4b5a854-7177-481e-b47a-41414a82ae4d"),
        Name = "John",
        FullName = "John Doe",
        Position = "Full Stack Developer",
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        AvatarUrl = "https://johndoe.com/avatar.png",
        SpotifyAccountName = "johndoe2000",
        UserId = User_1.Id,
        CreatedAt = new DateTime(2024, 4, 5, 14, 30, 0)
    };

    public static Author Author_2 => new()
    {
        Id = Guid.Parse("d92e6d4d-21db-40f8-8375-33d5466b5fc2"),
        Name = "Jane",
        FullName = "Jane Smith",
        Position = "Frontend Developer",
        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
        AvatarUrl = "https://janesmith.com/avatar.png",
        SpotifyAccountName = "janesmith99",
        UserId = User_2.Id,
        CreatedAt = new DateTime(2024, 4, 6, 10, 45, 0)
    };

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
