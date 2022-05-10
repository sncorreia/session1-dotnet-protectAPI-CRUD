using session1_protectAPI_CRUD.Models;

namespace session1_protectAPI_CRUD.Repositories
{
    public interface IWeatherForecastsRepository
    {
        Task<WeatherForecast> GetWeatherForecastByIdAsync(Guid id);
        Task<IEnumerable<WeatherForecast>> GetAllWeatherForecastsAsync();
        Task CreateWeatherForecastAsync(WeatherForecast weatherForecast);
        Task UpdateWeatherForecastAsync(WeatherForecast weatherForecast);
        Task DeleteWeatherForecastAsync(Guid id);
    }
}
