using Core;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ILogger<LocationsController> _logger;
        private readonly ILocationsService _locationsService;
        public LocationsController(ILocationsService locationsService, ILogger<LocationsController> logger)
        {
            _locationsService = locationsService;
            _logger = logger;
        }

        [HttpGet(Name = "GetCities")]
        public IEnumerable<string> GetCities()
        {
            return _locationsService.GetCities();
        }
    }
}
