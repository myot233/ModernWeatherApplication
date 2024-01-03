using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.Extensions;
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




        public WeatherViewPage(WeatherViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
            
        }
    }
}

