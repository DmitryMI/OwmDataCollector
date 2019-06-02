using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

                string text =
                    "Temperature (C): " + (data.Temperature - 273.15).ToString("0.0") + "\t\n" +
                    "Humidity (%): " + data.Humidity + "\t\n" +
                    "Sunrise time: " + data.SunriseTime.ToShortTimeString() + "\t\n" +
                    "Sunset time: " + data.SunsetTime.ToShortTimeString() + "\t\n" +
                    "Time: " + data.Timestamp;

                Console.WriteLine(text);

                Console.WriteLine();
                Console.WriteLine("Saving to file...");

                //string fileName = Regex.Escape(DateTime.Now.ToShortDateString());
                string fileName = DateTime.Now.ToShortDateString() + ".weather.txt";

                FileSaver.SaveToFile(text, fileName);
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

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
