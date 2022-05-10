using System.ComponentModel.DataAnnotations;

namespace session1_protectAPI_CRUD.Dtos
{
    public record UpdateWeatherForecastDto
    {
        [Required]
        public string City { get; init; }

        [Required]
        [Range(-100, 100)]
        public int TemperatureC { get; init; }

        [Required]
        public string Summary { get; init; }
    }
}
