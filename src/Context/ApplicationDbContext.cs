using PortfolioAPI.Entities;
using Microsoft.EntityFrameworkCore;
using PortfolioAPI.Mapping;
using PortfolioAPI.Seeds;

namespace PortfolioAPI.Context;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new UserSeed());
    }
}
