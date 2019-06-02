using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    public class WeatherGetter
    {
        public const string DefaultApiKey = "94281d08314e67032d83cc80d316a7d6";

        public enum WeatherField
        {
            Temperature, 
            Humidity,
            SunsetTime, 
            SunriseTime,
            DateTime
        }

        private readonly IJsonSerializer _serializer;
        private readonly IOwmApiHandler _apiHandler;
        private readonly string _customApiKey = null;

        public WeatherGetter(IJsonSerializer serializer, IOwmApiHandler apiHandler, string apiKey)
        {
            _serializer = serializer;
            _apiHandler = apiHandler;
            _customApiKey = apiKey;
        }

        public WeatherGetter()
        {
            _serializer = new JsonSerializerSimple();
            _apiHandler = new OwmApiHandlerSimple();
        }

        public WeatherData GetDataForCity(string cityName)
        {
            var key = _customApiKey ?? DefaultApiKey;

            return DownloadDataSynchronously(cityName, String.Empty, key);
        }

        public WeatherData GetDataForCity(string cityName, string countryCode)
        {
            var key = _customApiKey ?? DefaultApiKey;

            return DownloadDataSynchronously(cityName, countryCode, key);
        }

        private WeatherData DownloadDataSynchronously(string city, string country, string apiKey)
        {
            try
            {
                _apiHandler.RequestData(city, country, apiKey);

                while (!_apiHandler.IsReady)
                {
                    // Waiting for asynchronous operation
                }

                string rawData = _apiHandler.GetJson();
                _serializer.ParseJson(rawData);

                if(_serializer.GetResponseCode() != 200)
                    throw new DataWrongArguments("Response code is not equal to 200.");

                string tempStr = _serializer.GetFieldValue(WeatherField.Temperature);
                string humidityStr = _serializer.GetFieldValue(WeatherField.Humidity);
                string sunsetStr = _serializer.GetFieldValue(WeatherField.SunsetTime);
                string sunriseStr = _serializer.GetFieldValue(WeatherField.SunriseTime);
                string currentDt = _serializer.GetFieldValue(WeatherField.DateTime);

                long binarySunset = Int64.Parse(sunsetStr);
                long binarySunrise = Int64.Parse(sunriseStr);
                long binaryDt = Int64.Parse(currentDt);

                float temperature = Single.Parse(tempStr);
                float humidity = Single.Parse(humidityStr);
                DateTime sunset = UnixTimeStampToDateTime(binarySunset);
                DateTime sunrise = UnixTimeStampToDateTime(binarySunrise);
                DateTime timeStamp = UnixTimeStampToDateTime(binaryDt); ;

                WeatherData result = new WeatherData(temperature, humidity, sunset, sunrise, timeStamp);

                return result;
            }
            catch (FormatException ex)
            {
                throw new DataParsingException(ex.Message);
            }
            catch (WebException ex)
            {
                throw new DataDownloadingException(ex.Message);
            }
            
        }

        private static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
