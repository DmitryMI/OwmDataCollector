using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    internal struct Coord
    {
        public float Lon, Lat;
    }

    internal struct Weather
    {
        public int Id;
        public string Main;
        public string Description;
        public string Icon;
    }

    internal struct Main
    {
        public float Temp;
        public float Pressure;
        public float Humidity;
        public float TempMin;
        public float TempMax;
    }

    internal struct Wind
    {
        public float Speed;
        public float Deg;
    }

    internal struct Clouds
    {
        public float All;
    }

    internal struct Sys
    {
        public int Type;
        public int Id;
        public float Message;
        public string Country;
        public long Sunrise;
        public long Sunset;
    }

    internal struct OwnDataStructure
    {
        public Coord Coord;
        public Weather[] Weather;
        public string Base;
        public Main Main;
        public float Visibility;
        public Wind Wind;
        public Clouds Clouds;
        public long Dt;
        public Sys Sys;
        public int Timezone;
        public int Id;
        public string Name;
        public int Cod;
    }
}
