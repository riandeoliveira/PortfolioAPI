namespace PortfolioAPI.Entities;

public sealed class WeatherForecast : BaseEntity
{
  public string Summary { get; private set; } = "";

  public double TemperatureC { get; private set; }

  public double TemperatureF { get; private set; }

  public DateTime Date { get; private set; }
}
