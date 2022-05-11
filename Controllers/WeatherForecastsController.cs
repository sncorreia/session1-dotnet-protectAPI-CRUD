using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using session1_protectAPI_CRUD.Dtos;
using session1_protectAPI_CRUD.Models;
using session1_protectAPI_CRUD.Repositories;

namespace session1_protectAPI_CRUD.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastsController : ControllerBase
    {
        private readonly IWeatherForecastsRepository _repository;

        public WeatherForecastsController(IWeatherForecastsRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] // GET /weatherForecasts
        public async Task<IEnumerable<WeatherForecastDto>> GetWeatherForecastsAsync()
        {
            var weatherForecasts = (await _repository.GetAllWeatherForecastsAsync()).Select(x => x.AsDto());
            return weatherForecasts;
        }

        [HttpGet("{id}")] // GET /weatherForecasts/{id}
        public async Task<ActionResult<WeatherForecastDto>> GetWeatherForecastAsync(Guid id)
        {
            var weatherForecast = await _repository.GetWeatherForecastByIdAsync(id);

            if(weatherForecast is null)
            {
                return NotFound();
            }
            return Ok(weatherForecast);
        }

        [HttpPost] // POST /weatherForecasts
        public async Task<ActionResult<WeatherForecastDto>> CreateWeatherForecastAsync(CreateWeatherForecastDto weatherForecastDto)
        {
            WeatherForecast weatherForecast = new()
            {
                Id = Guid.NewGuid(),
                Date = DateTimeOffset.UtcNow,
                City = weatherForecastDto.City,
                Summary = weatherForecastDto.Summary,
                TemperatureC = weatherForecastDto.TemperatureC
            };

            await _repository.CreateWeatherForecastAsync(weatherForecast);

            return CreatedAtAction(nameof(GetWeatherForecastAsync), new { id = weatherForecast.Id }, weatherForecast.AsDto());
        }

        [HttpPut("{id}")] // PUT /weatherForecasts/{id}
        public async Task<ActionResult> UpdateWeatherForecastAsync(Guid id, UpdateWeatherForecastDto weatherForecastDto)
        {
            var existingWeatherForecast = await _repository.GetWeatherForecastByIdAsync(id);
            if(existingWeatherForecast is null)
            {
                return NotFound();
            }

            WeatherForecast updatedWeatherForecast = existingWeatherForecast with
            {
                City = weatherForecastDto.City,
                Summary = weatherForecastDto.Summary,
                TemperatureC = weatherForecastDto.TemperatureC
            };

            await _repository.UpdateWeatherForecastAsync(updatedWeatherForecast);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteWeatherForecastAsync(Guid id)
        {
            var existingWeatherForecast = await _repository.GetWeatherForecastByIdAsync(id);
            if (existingWeatherForecast is null)
            {
                return NotFound();
            }

            await _repository.DeleteWeatherForecastAsync(id);

            return NoContent();
        }
    }
}