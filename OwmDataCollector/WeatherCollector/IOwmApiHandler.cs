using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    public interface IOwmApiHandler
    {
        void RequestData(string cityCode, string countryCode, string apiKey);

        bool IsReady { get; }

        string GetJson();
    }
}
