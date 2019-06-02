using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace OwmDataCollector.WeatherCollector
{
    class JsonSerializerSimple : IJsonSerializer
    {
        private OwnDataStructure _data;
        private int _responseCode;
        private string _responseMessage;

        public void ParseJson(string json)
        {
            
            try
            {
                JObject jObject = JObject.Parse(json);

                // Firstly we will get response code and error message
                _responseCode = jObject["cod"].Value<int>();

                if (jObject.TryGetValue("message", out var message))
                {
                    _responseMessage = message.ToString();
                }
                else
                {
                    _responseMessage = String.Empty;
                }

                _data = new OwnDataStructure
                {
                    Main = new Main
                    {
                        Humidity = Single.Parse(jObject["main"]["humidity"].ToString()),
                        Temp = Single.Parse(jObject["main"]["temp"].ToString()),
                    },
                    Sys = new Sys
                    {
                        Sunrise = Int64.Parse(jObject["sys"]["sunrise"].ToString()),
                        Sunset = Int64.Parse(jObject["sys"]["sunset"].ToString())
                    },
                    Dt = Int64.Parse(jObject["dt"].ToString())
                };
            }
            catch (Exception e)
            {
                
            }
        }

        public string GetFieldValue(WeatherGetter.WeatherField field)
        {
            switch (field)
            {
                case WeatherGetter.WeatherField.Temperature:
                    return _data.Main.Temp.ToString();
                    break;
                case WeatherGetter.WeatherField.Humidity:
                    return _data.Main.Humidity.ToString();
                    break;
                case WeatherGetter.WeatherField.SunsetTime:
                    return _data.Sys.Sunset.ToString();
                    break;
                case WeatherGetter.WeatherField.SunriseTime:
                    return _data.Sys.Sunrise.ToString();
                    break;
                case WeatherGetter.WeatherField.DateTime:
                    return _data.Dt.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(field), field, null);
            }
        }

        public bool HasField(WeatherGetter.WeatherField field)
        {
            if(_responseCode == 200)
            switch (field)
            {
                case WeatherGetter.WeatherField.Temperature:
                    return true;
                    break;
                case WeatherGetter.WeatherField.Humidity:
                    return true;
                    break;
                case WeatherGetter.WeatherField.SunsetTime:
                    return true;
                    break;
                case WeatherGetter.WeatherField.SunriseTime:
                    return true;
                    break;
                case WeatherGetter.WeatherField.DateTime:
                    return true;
                default:
                    return false;
            }

            return false;
        }

        public int GetResponseCode()
        {
            return _responseCode;
        }

        public string GetResponseMessage()
        {
            return _responseMessage;
        }
    }
}
