using Microsoft.AspNetCore.Mvc;
using Solar.Services;

namespace Solar.Controllers;

[ApiController]
[Route("[controller]")]


public class WeatherController : ControllerBase
{
    private readonly IJsonParser _jsonParser;
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService, IJsonParser iJsonParser)
    {
        _weatherService = weatherService;
        _jsonParser = iJsonParser;
    }

    [HttpGet("times")]

    public async Task<ActionResult<SunTimes>> GetTimes(string cityName)
    {

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

