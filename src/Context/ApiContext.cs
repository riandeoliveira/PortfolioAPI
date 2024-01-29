using PortfolioAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace PortfolioAPI.Context;

public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
  public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();
}
