namespace session1_protectAPI_CRUD.Models
{
    public record WeatherForecast
    {
        public Guid Id { get; init; }
        public string City { get; init; }
        public DateTimeOffset Date { get; init; }
        public int TemperatureC { get; init; }
        public string Summary { get; init; }
    }
}

