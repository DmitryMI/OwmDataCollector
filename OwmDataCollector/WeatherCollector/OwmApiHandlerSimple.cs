using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    class OwmApiHandlerSimple : IOwmApiHandler
    {
        //private const string FormatUrl = "api.openweathermap.org/data/2.5/weather?q={0}&appid={1}";
        private const string UriHost = "api.openweathermap.org";
        private const string UriPath = "/data/2.5/weather";
        private const string UriQueryString = "q={0}&appid={1}";

        private bool _isReady;
        private string _json;

        public string GetJson()
        {
            string unescape = Regex.Unescape(_json);
            return unescape;
        }

        public void RequestData(string cityCode, string countryCode, string apiKey)
        {
            _isReady = false;

            WebClient webClient = new WebClient();

            string url;
            string cityFull;
            if (String.IsNullOrEmpty(countryCode))
                cityFull = cityCode;
            else
                cityFull = cityCode + "," + countryCode;

            string query = String.Format(UriQueryString, cityFull, apiKey);
            UriBuilder builder = new UriBuilder("http", UriHost, 80, UriPath);
            builder.Query = query;

            byte[] data = webClient.DownloadData(builder.Uri);

            _json = Encoding.UTF8.GetString(data);

            _isReady = true;
        }

        public bool IsReady => _isReady;

        public OwmApiHandlerSimple()
        {

        }
    }
}
