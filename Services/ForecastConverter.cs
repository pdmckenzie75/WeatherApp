using Core;
using Core.OpenMateo;

namespace Services
{
    public static class ForecastConverter
    {
        public static WeekForecast CreateWeatherForecastFromMateoWeatherForecast(string cityName, MateoWeatherForecast forecast)
        {
            WeekForecast weekForecast = new() { City = cityName };

            foreach (var item in forecast.daily.time)
            {
                int index = forecast.daily.time.ToList().IndexOf(item);
                DailyForecast dailyForecast = new()
                {
                    Date = DateOnly.Parse(item),
                    MaxTemperatureC = forecast.daily.temperature_2m_max[index],
                    MinTemperatureC = forecast.daily.temperature_2m_min[index],
                    Rainfall = forecast.daily.rain_sum[index],
                    WindSpeedMph = forecast.daily.wind_speed_10m_max[index],
                    WindDirection = forecast.daily.wind_direction_10m_dominant[index],
                };

                weekForecast.DailyForecasts.Add(dailyForecast);
            }

            return weekForecast;
        }
    }
}
