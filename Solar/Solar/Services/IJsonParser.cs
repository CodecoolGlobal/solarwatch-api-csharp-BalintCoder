namespace Solar.Services;

public interface IJsonParser
{
    public City ParseCityJson(string json);
    public SunTimes ParseSunriseSunSetJson(string json, int cityId);
}