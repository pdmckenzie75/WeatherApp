using Core.OpenMateo;
using System.Text.Json;

namespace Services
{
    public class OpenMateoAPIService 
    {
        private readonly HttpClient _httpClient;

        public OpenMateoAPIService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.open-meteo.com");
        }

        public async Task<MateoWeatherForecast?> GetDailyForecastAsync(double latitude, double longitude)
        {
            // OpenMeteo API docs: https://open-meteo.com/en/docs
            var url = $"/v1/forecast?latitude={latitude}&longitude={longitude}&daily=temperature_2m_max,temperature_2m_min,rain_sum,wind_speed_10m_max,wind_direction_10m_dominant,wind_gusts_10m_max,showers_sum&timezone=Europe%2FLondon";
            try
            {
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                    return null;
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<MateoWeatherForecast>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                return null;
            }
        }
    }
}
