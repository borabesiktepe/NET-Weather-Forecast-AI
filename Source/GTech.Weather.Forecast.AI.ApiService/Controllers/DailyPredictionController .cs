using Microsoft.AspNetCore.Mvc;
using GTech.Weather.Forecast.AI.Integration;
using GTech.Weather.Forecast.AI.Domain.MLNET;

namespace GTech.Weather.Forecast.AI.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DailyPredictionController : Controller
    {
        private readonly ILogger<DailyPredictionController> _logger;

        public DailyPredictionController(ILogger<DailyPredictionController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetDailyPrediction")]
        public async Task<IEnumerable<PredictedDailyTemperature>> Get(string cityName, int horizon)
        {
            WeatherForecastMongoDBService mongoService = new();
            await mongoService.ClearDailyForecastCollection();
            await mongoService.InsertDailyForecastCollection(cityName);

            List<PredictedDailyTemperature> requests = new();
            TimeSeriesService service = new TimeSeriesService();
            
            requests.Add(service.GetMLAsync(cityName, horizon));
            return requests;
        }
    }
}
