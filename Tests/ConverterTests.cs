using Core;
using Core.OpenMateo;
using Services;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void BasicForecastConverterTest()
        {
            MateoWeatherForecast forecast = new()
            {
                daily = new Daily()
                {
                    time = ["03/06/2025", "04/06/2025", "05/06/2025"],
                    temperature_2m_max = [19, 20, 20],
                    temperature_2m_min = [4,3,2],
                    rain_sum = [0,4.5f,0],
                    wind_speed_10m_max = [10,12,13],
                    wind_direction_10m_dominant = [90,100,75]
                }
            };

            WeekForecast weekForecast = ForecastConverter.CreateWeatherForecastFromMateoWeatherForecast("London", forecast);

            Assert.That(weekForecast.DailyForecasts.Count, Is.EqualTo(3));

            Assert.That(weekForecast.DailyForecasts[0].Date, Is.EqualTo(DateOnly.Parse("03/06/2025")));
            Assert.That(weekForecast.DailyForecasts[0].MaxTemperatureC, Is.EqualTo(19));
            Assert.That(weekForecast.DailyForecasts[0].MinTemperatureC, Is.EqualTo(4));
            Assert.That(weekForecast.DailyForecasts[0].Rainfall, Is.EqualTo(0));
            Assert.That(weekForecast.DailyForecasts[0].WindSpeedMph, Is.EqualTo(10));
            Assert.That(weekForecast.DailyForecasts[0].WindDirection, Is.EqualTo(90));

            Assert.That(weekForecast.DailyForecasts[1].Date, Is.EqualTo(DateOnly.Parse("04/06/2025")));
            Assert.That(weekForecast.DailyForecasts[1].MaxTemperatureC, Is.EqualTo(20));
            Assert.That(weekForecast.DailyForecasts[1].MinTemperatureC, Is.EqualTo(3));
            Assert.That(weekForecast.DailyForecasts[1].Rainfall, Is.EqualTo(4.5f));
            Assert.That(weekForecast.DailyForecasts[1].WindSpeedMph, Is.EqualTo(12));
            Assert.That(weekForecast.DailyForecasts[1].WindDirection, Is.EqualTo(100));

            Assert.That(weekForecast.DailyForecasts[2].Date, Is.EqualTo(DateOnly.Parse("05/06/2025")));
            Assert.That(weekForecast.DailyForecasts[2].MaxTemperatureC, Is.EqualTo(20));
            Assert.That(weekForecast.DailyForecasts[2].MinTemperatureC, Is.EqualTo(2));
            Assert.That(weekForecast.DailyForecasts[2].Rainfall, Is.EqualTo(0));
            Assert.That(weekForecast.DailyForecasts[2].WindSpeedMph, Is.EqualTo(13));
            Assert.That(weekForecast.DailyForecasts[2].WindDirection, Is.EqualTo(75));

            Assert.Pass();
        }
    }
}
