using Microsoft.AspNetCore.Mvc;
using Solar.Data;
using Solar.Services;

namespace Solar.Controllers;

[ApiController]
[Route("[controller]")]


public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;
    private readonly IJsonParser _jsonParser;
    private readonly ICityRepository _cityRepository;

    public WeatherController(IWeatherService weatherService, IJsonParser iJsonParser, ICityRepository cityRepository)
    {
        _weatherService = weatherService;
        _jsonParser = iJsonParser;
        _cityRepository = cityRepository;
    }

    [HttpGet("times")]

    public async Task<ActionResult<SunTimes>> GetTimes(string cityName)
    {
        var city = _cityRepository.GetByName(cityName);
        
        if (city == null)
        {
            return NotFound($"The city: {city.Name} not found");
        }

        try
        {
            var LatlngJson = await  _weatherService.GetLetLng(cityName);

            (double lat, double lon) =   _jsonParser.ParseCordJson(LatlngJson);


            var sunRiseSunSetJson = await _weatherService.GetSunriseAndSunset(lat, lon);

            return Ok(_jsonParser.ParseSunriseSunSetJson(sunRiseSunSetJson, cityName));

        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
    }
    
}

