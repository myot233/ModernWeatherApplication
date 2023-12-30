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
        private DayWeatherData _dayWeatherData;
        public string Date
        {
            get
            {
                var dateTime = DateTime.ParseExact(_dayWeatherData.fxDate, "yyyy-MM-dd", null);
                return $"{dateTime:ddddd} {dateTime:dd}";
            }
        }

        public Uri Image => new(int.Parse(_dayWeatherData.iconDay) switch
        {

            100 => "pack://application:,,,/Resources/sunny.svg",
            101 => "pack://application:,,,/Resources/cloudy.svg",
            104 => "pack://application:,,,/Resources/overcast.svg",
            300 => "pack://application:,,,/Resources/shower.svg",
            301 => "pack://application:,,,/Resources/shower.svg",
            > 302 and < 305 => "pack://application:,,,/Resources/thunder_shower.svg",
            305 => "pack://application:,,,/Resources/rain_small.svg",
            _ => "pack://application:,,,/Resources/sunny.svg"
        });

        public string MinTemp => _dayWeatherData.tempMin + "℃";

        public string MaxTemp => _dayWeatherData.tempMax + "℃";

        public string WindSpeedDay => _dayWeatherData.windSpeedDay + "公里/小时";

        public string WindDirectionDay => _dayWeatherData.windDirDay;

        public WeatherModel(DayWeatherData data)
        {
           
            _dayWeatherData = data;
        }
    }
}
