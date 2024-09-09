using System.Globalization;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Solar.Services;

public class JsonParser : IJsonParser
{
    public City ParseCityJson(string json)
    {
        var document = JsonDocument.Parse(json);

        var city = document.RootElement[0];

        var name = city.GetProperty("name").GetString();

        var lat = city.GetProperty("lat").GetDouble();

        var lng = city.GetProperty("lon").GetDouble();
        
        var state = city.TryGetProperty("state", out var stateProperty) ? stateProperty.GetString() : null; // Handle null state
        var country = city.GetProperty("country").GetString();

        return new City
        {
            Name = name ?? string.Empty,
            Latitude = lat,
            Longitude = lng,
            State = state,
            Country = country ?? string.Empty
        };

    }

    public SunTimes ParseSunriseSunSetJson(string json, int cityId)
    {
        var document = JsonDocument.Parse(json);
        
        var main = document.RootElement.GetProperty("results");
        Console.WriteLine(main.GetProperty("sunrise").GetString());
        var sunrise = DateTime.ParseExact(main.GetProperty("sunrise").GetString() ?? "", "h:mm:ss tt", CultureInfo.InvariantCulture);
        var sunset = DateTime.ParseExact(main.GetProperty("sunset").GetString() ?? "", "h:mm:ss tt", CultureInfo.InvariantCulture);
        var timezone = document.RootElement.GetProperty("tzid").GetString();

        return new SunTimes()
        {
            
            Date = DateTime.Now,
            Sunrise = sunrise,
            Sunset = sunset,
            TimeZone = timezone,
            CityId = cityId
        };
    }
}