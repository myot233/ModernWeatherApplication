using System.Windows.Media;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using ModernWeatherApplication.Service;
using ModernWeatherApplication.ViewModel;
using SkiaSharp;

namespace ModernWeatherApplication.Model
{
    public class WeatherModel
    {
        private readonly DayWeatherData _dayWeatherData;


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

        public string MinTemp => _dayWeatherData.tempMin + " ℃";

        public string MaxTemp => _dayWeatherData.tempMax + " ℃";

        public string WindSpeedDay => _dayWeatherData.windSpeedDay + " 公里/小时";

        public string WindDirectionDay => _dayWeatherData.windDirDay;

        public string Pressure => _dayWeatherData.pressure + " 百帕";

        public string Precip => _dayWeatherData.precip + " 毫米";

        public string Vis => _dayWeatherData.vis + " 公里";

        public string Humidity => _dayWeatherData.humidity + " %";

        public string SunRise => _dayWeatherData.sunrise;
        
        public string SunSet => _dayWeatherData.sunset;
        public IEnumerable<ISeries> Pie
        {
            get
            {
                var span = DateTime.ParseExact(_dayWeatherData.sunset, "HH:mm", null) -
                           DateTime.ParseExact(_dayWeatherData.sunrise, "HH:mm", null);
                return new[]
                {
                    new PieSeries<int>
                    {
                        Name = "日间",
                        
                        ToolTipLabelFormatter = (x) => _dayWeatherData.sunrise,
                        Values = new[]{  span.Minutes + span.Hours * 60 },
                        Fill = new SolidColorPaint(SKColors.LightYellow)
                    },
                    new PieSeries<int>
                    {
                        Name = "夜间",
                        ToolTipLabelFormatter = (x) => _dayWeatherData.sunset,
                        Values = new []{ 24 * 60 - (span.Minutes + span.Hours * 60) },
                        Fill = new SolidColorPaint(SKColors.LightBlue)
                    }
                    
                };
                


            }
        }

        public WeatherModel(DayWeatherData data)
        {
           
            _dayWeatherData = data;
            
        }
    }
}
