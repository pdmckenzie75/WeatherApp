using Core;
using Core.Interfaces;
using Core.OpenMateo;
using Data;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly WeatherCache _weatherCache;
        private readonly OpenMateoAPIService _openMateoAPIService;
        private readonly ILocationsService _locationsService;
        private readonly ILogger<WeatherForecastService> _logger;

        public WeatherForecastService(WeatherCache weatherCache, OpenMateoAPIService openMateoAPIService, ILocationsService locationsService, ILogger<WeatherForecastService> logger)
        {
            _weatherCache = weatherCache;
            _openMateoAPIService = openMateoAPIService;
            _locationsService = locationsService;
            _logger = logger;
        }

        public async Task<List<WeekForecast>> GetWeekForecast(List<string> cities)
        {
            List<WeekForecast> weekForecasts = new List<WeekForecast>();

            foreach (var cityName in cities)
            {
                var cachedWeather = _weatherCache.TryGetForecast(cityName);
                if (cachedWeather != null)
                {
                    _logger.LogInformation($"Returning cached weather data for city '{cityName}'.");
                    weekForecasts.Add(cachedWeather);
                }
                else
                {
                    _logger.LogInformation($"Retrieving weather data from Open Mateo for city '{cityName}'.");

                    WeekForecast? weekForecast = await GetWeekForecastFromMateo(cityName);

                    if (weekForecast != null)
                    {
                        _weatherCache.AddForecast(cityName, weekForecast);
                        weekForecasts.Add(weekForecast);
                    }
                }
            }

            return weekForecasts;
        }

        private async Task<WeekForecast?> GetWeekForecastFromMateo(string cityName)
        {
            var location = _locationsService.GetCityLocation(cityName);
            if (location == null)
            {
                _logger.LogError($"Location for city '{cityName}' not found.");
                return null;
            }

            MateoWeatherForecast? forecast = await _openMateoAPIService.GetDailyForecastAsync(location.Value.latitude, location.Value.longitude);

            if (forecast == null)
            {
                _logger.LogError("Failed to fetch weather data from OpenMateo API.");
                return null;
            }

            WeekForecast weekForecast = ForecastConverter.CreateWeatherForecastFromMateoWeatherForecast(cityName, forecast);

            return weekForecast;
        }

       
    }
}
