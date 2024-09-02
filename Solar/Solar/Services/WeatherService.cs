using System.Net;
using Solar.Services;

namespace Solar;

//  47.4979937,
 //"lon": 19.0403594,

public class WeatherService : IWeatherService
{
    public async Task<string> GetSunriseAndSunset(double lat, double lon)
    {
        var url = $"https://api.sunrise-sunset.org/json?lat={lat}&lng={lon}";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> GetLetLng(string cityName)
    {
        var apiKey = "f778716bc3508229848e8235af6fca4e";
        var url = $"http://api.openweathermap.org/geo/1.0/direct?q={cityName}&appid={apiKey}";

        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}