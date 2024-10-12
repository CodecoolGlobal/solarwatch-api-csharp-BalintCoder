using Moq;
using Solar.Controllers;
using Solar.Services;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SolarWatchTest
{
    public class WeatherControllerTests
    {
        private readonly Mock<IWeatherService> _mockWeatherService;
        private readonly Mock<IJsonParser> _mockJsonParser;
        private readonly WeatherController _controller;

        public WeatherControllerTests()
        {
            _mockWeatherService = new Mock<IWeatherService>();
            _mockJsonParser = new Mock<IJsonParser>();
            _controller = new WeatherController(_mockWeatherService.Object, _mockJsonParser.Object);
        }

        [Fact]
        public async Task GetTimes_ReturnsOkResult_WithValidData()
        {
            // Arrange
            var cityName = "TestCity";
            var lat = 47.4979937;
            var lon = 19.0403594;
            var latlngJson = "{\"lat\": 47.4979937, \"lon\": 19.0403594}";
            var sunTimesJson = "{\"results\": { \"sunrise\": \"6:30:00 AM\", \"sunset\": \"8:00:00 PM\" }, \"tzid\": \"Europe/Budapest\"}";
            var expectedSunTimes = new SunTimes { City = cityName, Date = DateOnly.FromDateTime(DateTime.Now), Sunrise = DateTime.Parse("6:30 AM"), Sunset = DateTime.Parse("8:00 PM"), TimeZone = "Europe/Budapest" };

            _mockWeatherService.Setup(ws => ws.GetLetLng(cityName)).ReturnsAsync(latlngJson);
            _mockJsonParser.Setup(jp => jp.ParseCordJson(latlngJson)).Returns((lat, lon));
            _mockWeatherService.Setup(ws => ws.GetSunriseAndSunset(lat, lon)).ReturnsAsync(sunTimesJson);
            _mockJsonParser.Setup(jp => jp.ParseSunriseSunSetJson(sunTimesJson, cityName)).Returns(expectedSunTimes);

            // Act
            var result = await _controller.GetTimes(cityName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<SunTimes>(okResult.Value);
            Assert.Equal(expectedSunTimes.City, returnValue.City);
            Assert.Equal(expectedSunTimes.Sunrise, returnValue.Sunrise);
            Assert.Equal(expectedSunTimes.Sunset, returnValue.Sunset);
            Assert.Equal(expectedSunTimes.TimeZone, returnValue.TimeZone);
        }

        [Fact]
        public async Task GetTimes_ReturnsNotFound_OnException()
        {
            // Arrange
            var cityName = "InvalidCity";
            _mockWeatherService.Setup(ws => ws.GetLetLng(cityName)).ThrowsAsync(new Exception("City not found"));

            // Act
            var result = await _controller.GetTimes(cityName);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            Assert.Equal("City not found", notFoundResult.Value);
        }
    }
}
