using CommunityToolkit.Mvvm.ComponentModel;
using ModernWeatherApplication.Views;

namespace ModernWeatherApplication.ViewModel
{
    public partial class MainViewModel: ObservableObject
    {
        [ObservableProperty]
        private string _applicationTitle = "天气";


        [ObservableProperty] 
        private Type _weatherPage = typeof(WeatherViewPage);

        [ObservableProperty] 
        private Type _settingPage = typeof(SettingViewPage);
    }
}
