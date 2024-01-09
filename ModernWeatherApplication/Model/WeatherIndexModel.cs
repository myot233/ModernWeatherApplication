using System.Windows.Media;
using ModernWeatherApplication.Service;

namespace ModernWeatherApplication.Model
{
     public class WeatherIndexModel
    {
        private readonly WeatherIndex _weatherIndex;

        public int Value => (int)(100 * ((MaxValue - int.Parse(_weatherIndex.level)) / (double)MaxValue ));

        public string Detail => _weatherIndex.text;

        public SolidColorBrush Color => Value switch
        {
            >= 0 and < 35 => new SolidColorBrush(Colors.Red),
            >= 35 and < 65 => new SolidColorBrush(Colors.Yellow),
            >= 65 and <= 100 => new SolidColorBrush(Colors.GreenYellow),
            _ => new SolidColorBrush(Colors.Violet) //异常颜色
        };
        public int MaxValue => int.Parse(_weatherIndex.type) switch
        {
            1 => 3,
            2 => 4,
            3 => 7,
            4 => 3,
            5 => 5,
            6 => 5,
            7 => 5,
            8 => 7,
            9 => 4,
            10 => 5,
            11 => 4,
            12 => 5,
            13 => 8,
            14 => 6,
            15 => 5,
            16 => 5,
            _ => throw new ArgumentOutOfRangeException()
        };

        public string Name => _weatherIndex.name;
        public WeatherIndexModel(WeatherIndex weatherIndex)
        {
            _weatherIndex = weatherIndex;
        }
    }
}
