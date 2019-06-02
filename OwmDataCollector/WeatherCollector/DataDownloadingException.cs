using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwmDataCollector.WeatherCollector
{
    class DataDownloadingException : Exception
    {
        public DataDownloadingException(string message) : base(message)
        {

        }
    }
}
