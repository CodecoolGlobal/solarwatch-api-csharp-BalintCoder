namespace Solar.Services;

public interface IJsonParser
{
    public (double, double) ParseCordJson(string json);
    public SunTimes ParseSunriseSunSetJson(string json, string cityName);
}