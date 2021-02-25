using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemoWithSwagger.Api.Controllers
{
  /// <summary>
  /// Provides a simple weather forecast.
  /// </summary>
  [ApiController]
  [Route("/api/[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private static readonly string[] Summaries = new[]
    {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
      _logger = logger;
    }

    /// <summary>
    /// Delivers 5 weather forecasts.
    /// </summary>
    /// <returns>the next weather forecasts.</returns>
    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
      var rng = new Random();
      return Enumerable.Range(1, 5).Select(index => new WeatherForecast
      {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
      })
      .ToArray();
    }

    /// <summary>
    /// Delivers historical data
    /// </summary>
    /// <param name="timestamp">a timestamp in the past</param>
    /// <returns>the weather in the past</returns>
    [HttpGet("{timestamp}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<WeatherForecast> Get(DateTime timestamp)
    {
      if(timestamp >= DateTime.Now)
      {
        return NotFound();
      }

      var rng = new Random();
      return Ok(new WeatherForecast
      {
        Date = timestamp,
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
      });
    }
  }
}
