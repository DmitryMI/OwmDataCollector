using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    class DataParsingException : Exception
    {
        public DataParsingException(string message) : base(message)
        {

        }
    }
}
