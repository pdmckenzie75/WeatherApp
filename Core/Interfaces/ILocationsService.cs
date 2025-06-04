namespace Core.Interfaces
{
    public interface ILocationsService
    {
        IEnumerable<string> GetCities();

        (double latitude, double longitude)? GetCityLocation(string cityName);
    }
}
