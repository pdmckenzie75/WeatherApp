using Core;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _weatherForecastService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(IWeatherForecastService weatherForecastService, ILogger<WeatherForecastController> logger)
        {
            _weatherForecastService = weatherForecastService;
            _logger = logger;
        }

        [HttpGet()]
        public List<WeekForecast> Get([FromQuery] List<string> cities)
        {
            return _weatherForecastService.GetWeekForecast(cities).Result;
        }
    }
}
