using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    class DataWrongArguments : Exception
    {
        public DataWrongArguments(string message) : base(message)
        {

        }
    }
}
