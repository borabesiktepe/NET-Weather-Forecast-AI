using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
using Newtonsoft.Json;

namespace GTech.Weather.Forecast.AI.Integration
{
    //Get data from "5 Days of Daily Forecasts" by the city entered

    public class DailyForecastsService
    {
        private readonly HttpClient _Client = new();
        public string ApiKey = GetApiKey.Update();
        public string BaseUrl = "http://dataservice.accuweather.com/";

        public async Task<List<DailyForecast>> GetDailyForecastsAsync(string cityName)
        {
            var service = new AccuWeatherService();
            var result = await service.GetCityKeyAsync(cityName);

            string FullUrl = $"{BaseUrl}forecasts/v1/daily/5day/{result}?apikey={ApiKey}&language=en-us&metric=true";
            string response = await _Client.GetStringAsync(FullUrl);

            Root city = JsonConvert.DeserializeObject<Root>(response);
            return city.DailyForecasts;
        }
    }
}