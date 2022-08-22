using GTech.Weather.Forecast.AI.Domain.WeatherForecast;
using MongoDB.Driver;
using Microsoft.ML;
using GTech.Weather.Forecast.AI.Infrastructure;
using Microsoft.ML.Transforms.TimeSeries;
using GTech.Weather.Forecast.AI.Domain.MLNET;

namespace GTech.Weather.Forecast.AI.Integration
{
    public class TimeSeriesService
    {
        MLContext context = new();
        WeatherForecastMongoDB connection = new();
        public PredictedDailyTemperature GetMLAsync(string cityName, int horizon)
        {
            var collection = connection.WeatherForecastCollection();
            var documents = collection.Find(FilterDefinition<DailyForecast>.Empty).ToEnumerable();
            List<MLDailyData> listMlDailyForecast = documents.Select(i => new MLDailyData{
                Value = (float)(i.Temperature.Maximum.Value),
                Date = (DateTime)i.Date
            }).ToList();
            
            var data = context.Data.LoadFromEnumerable(listMlDailyForecast);
            var pipeline = context.Forecasting.ForecastBySsa(
                nameof(MLDailyForecast.Forecast),
                nameof(MLDailyData.Value),
                windowSize: 2,
                seriesLength: 4,
                trainSize: listMlDailyForecast.Count,
                horizon: horizon
            );

            var model = pipeline.Fit(data);
            var forecastingEngine = model.CreateTimeSeriesEngine<MLDailyData, MLDailyForecast>(context);
            var forecasts = forecastingEngine.Predict().Forecast.ToList();

            List<Temperatures> temps = forecasts.Select((value, index) => new Temperatures
            {
                Temperature = value,
                Date = DateTime.Today.AddDays(5+index)
            }).ToList();

            var predict = new PredictedDailyTemperature
            {
                cityName = cityName,
                Temperatures = temps
            };

            return predict;
        }
    }
} 