using GTech.Weather.Forecast.AI.Infrastructure;

namespace GTech.Weather.Forecast.AI.Integration
{
    //MongoDB database operations

    public class WeatherForecastMongoDBService
    {
        public async Task ClearDailyForecastCollection()
        {
            var connection = new WeatherForecastMongoDB();
            var collection = connection.WeatherForecastCollection();
            await collection.DeleteManyAsync("{}");
        }

        public async Task InsertDailyForecastCollection(string cityName)
        {
            var service = new DailyForecastsService();
            var connection = new WeatherForecastMongoDB();
            var collection = connection.WeatherForecastCollection();
            var dailyForecastBSON = service.GetDailyForecastsAsync(cityName).Result;
            await collection.InsertManyAsync(dailyForecastBSON);
        }
    }
}