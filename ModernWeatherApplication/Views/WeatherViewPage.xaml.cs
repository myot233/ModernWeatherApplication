using LiveChartsCore.SkiaSharpView.Painting;
using ModernWeatherApplication.Controls;
using ModernWeatherApplication.ViewModel;
using SkiaSharp;
using Wpf.Ui.Controls;

namespace ModernWeatherApplication.Views
{
    /// <summary>
    /// WeatherViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherViewPage:INavigableView<WeatherViewModel>
    {
        public WeatherViewModel ViewModel { get; }

        public Paint Paint { get;  } = 
            new SolidColorPaint
        {
            Color = SKColors.DarkSlateGray,
            SKTypeface = SKFontManager.Default.MatchCharacter('汉')
        };


        public WeatherViewPage(WeatherViewModel viewModel)
        {
            
            ViewModel = viewModel;
            viewModel.LabelPaint = Paint;
            DataContext = this;
            InitializeComponent();
            
        }
    }
}

