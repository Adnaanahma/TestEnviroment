using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace TestingEnvironmetalVAr.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ConnectionStrings _connectionStrings;
    private readonly IConfiguration _configuration;
    public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<ConnectionStrings> connectionStrings, IConfiguration configuration)
    {
        _connectionStrings = connectionStrings.Value;
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("get-database-url")]
    public IActionResult GetDatabaseUrl()
    {
        //var databaseUrl = _connectionStrings.DefaultConnection;
        var databaseUrl = _configuration.GetValue<string>("DATABASE_URL");
        if (string.IsNullOrEmpty(databaseUrl))
        {
            return NotFound("Environment variable 'DATABASE_URL' not set.");
        }

        return Ok(new { DatabaseUrl = databaseUrl });
    }

}
