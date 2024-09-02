namespace Solar.Services;

public interface IWeatherService
{
    Task<string> GetSunriseAndSunset(double lat, double lon);

    Task<string> GetLetLng(string cityName);
}