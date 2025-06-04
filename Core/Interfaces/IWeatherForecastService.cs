namespace Core.Interfaces
{
    public interface IWeatherForecastService
    {
        Task<List<WeekForecast>> GetWeekForecast(List<string> cities);
    }
}
