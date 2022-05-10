using session1_protectAPI_CRUD.Models;

namespace session1_protectAPI_CRUD.Repositories
{
    public class InMemWeatherForecastsRepository : IWeatherForecastsRepository
    {
        private readonly List<WeatherForecast> _forecasts = new()
        {
            new WeatherForecast { Id = Guid.NewGuid(), City = "Lisbon", Date = DateTimeOffset.UtcNow, TemperatureC = 32, Summary = "Very hot summer day" },
            new WeatherForecast { Id = Guid.NewGuid(), City = "London", Date = DateTimeOffset.UtcNow, TemperatureC = 18, Summary = "Cloudy and with some expected rain" },
            new WeatherForecast { Id = Guid.NewGuid(), City = "New York", Date = DateTimeOffset.UtcNow, TemperatureC = 3, Summary = "Freezing and snow expected" },
        };

        public async Task CreateWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            _forecasts.Add(weatherForecast);
            await Task.CompletedTask;
        }

        public async Task DeleteWeatherForecastAsync(Guid id)
        {
            var index = _forecasts.FindIndex(existingItem => existingItem.Id == id);
            _forecasts.RemoveAt(index);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<WeatherForecast>> GetAllWeatherForecastsAsync()
        {
            return await Task.FromResult(_forecasts);
        }

        public async Task<WeatherForecast> GetWeatherForecastByIdAsync(Guid id)
        {
            return await Task.FromResult(_forecasts.Where(forecast => forecast.Id == id).SingleOrDefault());
        }

        public async Task UpdateWeatherForecastAsync(WeatherForecast weatherForecast)
        {
            var index = _forecasts.FindIndex(existingItem => existingItem.Id == weatherForecast.Id);
            _forecasts[index] = weatherForecast;
            await Task.CompletedTask;
        }
    }
}
