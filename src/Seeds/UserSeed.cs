using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PortfolioAPI.Entities;

namespace PortfolioAPI.Seeds;

public class UserSeed : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        var user1 = new User
        (
            "name1",
            "Full Name 1",
            "Position 1",
            "Description 1",
            "https://example.com/avatar1.jpg",
            "username1"
        );

        var user2 = new User
        (
            "name2",
            "Full Name 2",
            "Position 2",
            "Description 2",
            "https://example.com/avatar2.jpg",
            "username2"
        );

        var user3 = new User
        (
            "name3",
            "Full Name 3",
            "Position 3",
            "Description 3",
            "https://example.com/avatar3.jpg",
            "username3"
        );

        builder.HasData(user1, user2, user3);
    }
}
