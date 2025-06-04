using Core.Interfaces;
using Data;
using Microsoft.Extensions.Logging;

namespace Services
{
    public class LocationsService : ILocationsService
    {
        private readonly WeatherDbContext _context;
        private readonly ILogger<LocationsService> _logger;

        public LocationsService(WeatherDbContext context, ILogger<LocationsService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<string> GetCities()
        {
            return _context.Cities.Select(x => x.Name).ToList(); ;
        }

        public (double latitude, double longitude)? GetCityLocation(string cityName)
        {
            var city = _context.Cities.FirstOrDefault(c => c.Name.Equals(cityName, StringComparison.OrdinalIgnoreCase));

            if (city != null)
            {
                return (city.Latitude, city.Longitude);
            }
            else
            {
                _logger.LogWarning($"City '{cityName}' not found in the list of cities.");
                return null;
            }
        }
    }
}
