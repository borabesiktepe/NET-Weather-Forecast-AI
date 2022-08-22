using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GTech.Weather.Forecast.AI.Integration
{
    //Get "Location Key" from entered city to be used in other API services

    public class AccuWeatherService
    {
        private readonly HttpClient _Client = new();
        public string ApiKey = GetApiKey.Update();
        public string BaseUrl = "http://dataservice.accuweather.com/";

        public async Task<string> GetCityKeyAsync(string cityName)
        {
            string FullUrl = $"{BaseUrl}locations/v1/cities/search?apikey={ApiKey}&q={cityName}";
            var response = await _Client.GetStringAsync(FullUrl);
            var city = JsonConvert.DeserializeObject<List<JObject>>(response);
            dynamic key = city.FirstOrDefault();

            return key["Key"];
        }
    }
}