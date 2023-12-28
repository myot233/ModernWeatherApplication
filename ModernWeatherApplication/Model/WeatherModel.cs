using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ModernWeatherApplication.Service;
using SharpVectors.Converters;
using SharpVectors.Renderers;

namespace ModernWeatherApplication.Model
{
    public class WeatherModel
    {
        private WeatherData _weatherData;
        public string Date
        {
            get
            {
                var dateTime = DateTime.ParseExact(_weatherData.fxDate, "yyyy-MM-dd", null);
                return $"{dateTime:ddddd} {dateTime:dd}";
            }
        }

        public Uri Image => new(int.Parse(_weatherData.iconDay) switch
        {

            100 => "pack://application:,,,/Resources/sunny.svg",
            101 => "pack://application:,,,/Resources/cloudy.svg",
            _ => "pack://application:,,,/Resources/sunny.svg"
        });

        public string MinTemp => _weatherData.tempMin += "℃";

        public string MaxTemp => _weatherData.tempMax += "℃";
        

        public WeatherModel(WeatherData data)
        {
           
            _weatherData = data;
        }
    }
}
