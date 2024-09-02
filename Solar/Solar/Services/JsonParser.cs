using System.Globalization;
using System.Text.Json;

namespace Solar.Services;

public class JsonParser : IJsonParser
{
    public (double, double) ParseCordJson(string json)
    {
        var document = JsonDocument.Parse(json);

        var city = document.RootElement[0];

        var lat = city.GetProperty("lat").GetDouble();
        
        var lng = city.GetProperty("lon").GetDouble();

        return (lat, lng);

    }

    public SunTimes ParseSunriseSunSetJson(string json, string cityName)
    {
        var document = JsonDocument.Parse(json);
        
        var main = document.RootElement.GetProperty("results");
        Console.WriteLine(main.GetProperty("sunrise").GetString());
        var sunrise = DateTime.ParseExact(main.GetProperty("sunrise").GetString() ?? "", "h:mm:ss tt", CultureInfo.InvariantCulture);
        var sunset = DateTime.ParseExact(main.GetProperty("sunset").GetString() ?? "", "h:mm:ss tt", CultureInfo.InvariantCulture);
        var timezone = document.RootElement.GetProperty("tzid").GetString();

        return new SunTimes()
        {
            City = cityName,
            Date = DateOnly.FromDateTime(DateTime.Now),
            Sunrise = sunrise,
            Sunset = sunset,
            TimeZone = timezone
        };
    }
}