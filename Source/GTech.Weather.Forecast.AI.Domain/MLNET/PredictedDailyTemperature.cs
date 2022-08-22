namespace GTech.Weather.Forecast.AI.Domain.MLNET
{
    public class PredictedDailyTemperature
    {
        public string cityName { get; set; }
        public List<Temperatures>? Temperatures { get; set; }
    }
    public class Temperatures
    {
        public float Temperature { get; set; }
        public DateTime Date { get; set; }
    }
}