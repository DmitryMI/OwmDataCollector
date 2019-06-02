using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    public struct WeatherData
    {
        public float Temperature { get; private set; }
        public float Humidity { get; private set; }
        public DateTime SunsetTime { get; private set; }
        public DateTime SunriseTime { get; private set; }
        public DateTime Timestamp { get; private set; }

        public WeatherData(float temperature, float humidity, DateTime sunset, DateTime sunrise, DateTime stamp)
        {
            Temperature = temperature;
            Humidity = humidity;
            SunriseTime = sunrise;
            SunsetTime = sunset;
            Timestamp = stamp;
        }
    }
}
