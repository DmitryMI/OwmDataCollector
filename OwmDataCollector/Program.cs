using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OwmDataCollector.WeatherCollector;

namespace OwmDataCollector
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Specify city name: ");
            string cityName = Console.ReadLine(); ;

            WeatherGetter getter = new WeatherGetter(new JsonSerializerSimple(), new OwmApiHandlerSimple(), WeatherGetter.DefaultApiKey);

            Console.WriteLine("Getting data...");
            try
            {
                WeatherData data = getter.GetDataForCity(cityName);

                Console.WriteLine("Temperature (С): " + (data.Temperature - 273.15).ToString("0.0"));
                Console.WriteLine("Humidity (%): " + data.Humidity);
                Console.WriteLine("Sunrise time: " + data.SunriseTime.ToShortTimeString());
                Console.WriteLine("Sunset time: " + data.SunsetTime.ToShortTimeString());
                Console.WriteLine("Time: " + data.Timestamp);

            }
            catch (DataParsingException e)
            {
                Console.WriteLine(e);
            }
            catch (DataDownloadingException e)
            {
                Console.WriteLine("Service not found. May be the arguments were incorrect");
            }
            catch (DataWrongArguments e)
            {
                Console.WriteLine("The argument was wrong. Try to change your request.");
            }

            Console.ReadKey();
        }
    }
}
