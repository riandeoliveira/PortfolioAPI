using PortfolioAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PortfolioAPI.Controllers;

[ApiController]
[Route("api/weather-forecast")]
public class WeatherForecastController : ControllerBase
{
  [HttpGet]
  public IActionResult Get()
  {
    return Ok();
  }
}
