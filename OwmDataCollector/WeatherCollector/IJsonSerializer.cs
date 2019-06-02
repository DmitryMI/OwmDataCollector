using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    public interface IJsonSerializer
    {
        void ParseJson(string json);
        string GetFieldValue(WeatherGetter.WeatherField field);
        bool HasField(WeatherGetter.WeatherField field);

        int GetResponseCode();
        string GetResponseMessage();
    }
}
