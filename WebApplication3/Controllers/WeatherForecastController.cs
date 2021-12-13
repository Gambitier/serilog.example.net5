using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;

namespace WebApplication3.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<WeatherForecast> Get()
        {
            WeatherForecast weatherForecast = new()
            {
                Date = DateTime.Now,
                TemperatureC = 12,
                Summary = $"summary generated at {DateTime.Now}"
            };

            _logger.LogInformation("this is being logged by DI instance {@weatherForecast}", weatherForecast);

            var v = new { Amount = 108, Message = "Hello" };

            _logger.LogInformation("object logging example {@objectValues} {uu} ", v,v);

            //Log.Information

            return weatherForecast;
        }
    }
}
