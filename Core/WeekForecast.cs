namespace Core
{
    public class WeekForecast
    {       
        public string City { get; set; } = string.Empty;

        public List<DailyForecast> DailyForecasts { get; set; } = new List<DailyForecast>();
    }
}
