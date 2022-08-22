using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GTech.Weather.Forecast.AI.Domain.WeatherForecast
{
    public class Root
    {
        public Headline? Headline { get; set; }
        public List<DailyForecast>? DailyForecasts { get; set; }
    }
    public class DailyForecast
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public string? Id { get; set; }
        public DateTime? Date { get; set; }
        public int? EpochDate { get; set; }
        public DailyTemperature? Temperature { get; set; }
        public Day? Day { get; set; }
        public Night? Night { get; set; }
        public List<string>? Sources { get; set; }
        public string? MobileLink { get; set; }
        public string? Link { get; set; }
    }
    public class Day
    {
        public int? Icon { get; set; }
        public string? IconPhrase { get; set; }
        public bool? HasPrecipitation { get; set; }
    }
    public class Headline
    {
        public DateTime? EffectiveDate { get; set; }
        public int? EffectiveEpochDate { get; set; }
        public int? Severity { get; set; }
        public string? Text { get; set; }
        public string? Category { get; set; }
        public object? EndDate { get; set; }
        public object? EndEpochDate { get; set; }
        public string? MobileLink { get; set; }
        public string? Link { get; set; }
    }
    public class Maximum
    {
        public double? Value { get; set; }
        public string? Unit { get; set; }
        public int? UnitType { get; set; }
    }
    public class Minimum
    {
        public double? Value { get; set; }
        public string? Unit { get; set; }
        public int UnitType { get; set; }
    }
    public class Night
    {
        public int? Icon { get; set; }
        public string? IconPhrase { get; set; }
        public bool? HasPrecipitation { get; set; }
    }
    public class DailyTemperature
    {
        public Minimum? Minimum { get; set; }
        public Maximum? Maximum { get; set; }
    }
}
