using session1_protectAPI_CRUD.Dtos;
using session1_protectAPI_CRUD.Models;

namespace session1_protectAPI_CRUD
{
    public static class Extensions
    {
        public static WeatherForecastDto AsDto(this WeatherForecast weatherForecast)
        {
            return new WeatherForecastDto
            {
                Id = weatherForecast.Id,
                City = weatherForecast.City,
                Date = weatherForecast.Date,
                Summary = weatherForecast.Summary,
                TemperatureC = weatherForecast.TemperatureC
            };
        }
    }
}
