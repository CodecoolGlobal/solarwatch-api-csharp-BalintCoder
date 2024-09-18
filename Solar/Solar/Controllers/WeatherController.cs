using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("times"), Authorize(Roles = "User, Admin")]

    public async Task<ActionResult<SunTimes>> GetTimes(string cityName)
    {
        var city = _cityRepository.GetByName(cityName);
        
        if (city == null)
        {
            var latlngJson = await _weatherService.GetLetLng(cityName);

            city = _jsonParser.ParseCityJson(latlngJson);
            _cityRepository.Add(city);
        }

        try
        {
            var sunriseSunsetJson = await _weatherService.GetSunriseAndSunset(city.Latitude, city.Longitude);
            return Ok(_jsonParser.ParseSunriseSunSetJson(sunriseSunsetJson, city.Id));

        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
    }
    
    [HttpGet("GetWeatherForecast"), Authorize(Roles = "Admin")]
    public ActionResult<string> Something(string asd)
    {
        return Ok(asd);
    }
    
}

