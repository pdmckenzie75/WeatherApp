using Core;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Services
{
    public class WeatherCache
    {
        private readonly ILogger<WeatherCache> _logger;
        private readonly MemoryCache _cache;
        public WeatherCache(ILogger<WeatherCache> logger)
        {
            _logger = logger;
            _cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = 1024 * 1024 * 100 // 100 MB
            });
        }

        public WeekForecast? TryGetForecast(string cityName)
        {
            _cache.TryGetValue(cityName, out WeekForecast? cachedForecast);

            return cachedForecast;
        }

        public void AddForecast(string cityName, WeekForecast forecast)
        {
            _cache.Set(cityName, forecast, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                Size = 1
            });

         
        }
    }
}
