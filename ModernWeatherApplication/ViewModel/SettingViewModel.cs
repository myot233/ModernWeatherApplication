using System.Text.Json;
using System.Text.Json.Nodes;
using CommunityToolkit.Mvvm.ComponentModel;
using ModernWeatherApplication.Properties;
using Wpf.Ui.Controls;
using System.Windows.Controls;
using ModernWeatherApplication.Model;
using Wpf.Ui.Appearance;

namespace ModernWeatherApplication.ViewModel
{
    public partial class SettingViewModel: ObservableObject, INavigationAware
    {
        public int Location => int.Parse(_jroot[Setting.Province]![Setting.City]![Setting.Region]!.GetValue<string>());

        public event Action onLocationChanged;

        
        //[ObservableProperty] private string _firstSelected = "北京市";
        //[ObservableProperty] private string _secondSelected = "北京市";
        //[ObservableProperty] private string _lastSelected = "北京";
        [ObservableProperty] private Setting _setting;

        [ObservableProperty] private List<string> _firstPlace;
        [ObservableProperty] private List<string> _secondPlace;
        [ObservableProperty] private List<string> _lastPlace;
        
        

        private readonly JsonObject _jroot =  JsonSerializer.Deserialize<JsonObject>(System.Text.Encoding.UTF8.GetString(Resources.location));
        public SettingViewModel(Setting setting)
        {
            Setting = setting;
           //Setting.Theme = Setting.Theme == ApplicationTheme.Unknown ? ApplicationThemeManager.GetAppTheme() : Setting.Theme;
           ApplicationThemeManager.Apply(Setting.Theme);
            onLocationChanged += async () =>
            {
                await Setting.SaveToFileAsync();
            };
            FirstPlace = _jroot.Select(x => x.Key).ToList();
            

        }
        
       
        public void OnFirstSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)

        {
            SecondPlace = (_jroot[Setting.Province] as JsonObject)?.Select(x => x.Key).ToList() ?? new List<string>();
            Setting.City = SecondPlace[0];
            

        }


        public void OnSecondSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (!_jroot.ContainsKey(Setting.Province)) return;
            if (!(_jroot[Setting.Province] as JsonObject)!.ContainsKey(Setting.City ?? "")) return;
            LastPlace = (_jroot[Setting.Province]?[Setting.City] as JsonObject)?.Select(x => x.Key)
                .ToList() ?? new List<string>();
            Setting.Region = LastPlace[0];
            
        }

        public void OnLastSeleceted(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (!_jroot.ContainsKey(Setting.Province)) return;
            if (!(_jroot[Setting.Province] as JsonObject)!.ContainsKey(Setting.City ?? "")) return;
            if (Setting.City != null && !(_jroot[Setting.Province]?[Setting.City] as JsonObject)!.ContainsKey(Setting.Region ?? "")) return;
            onLocationChanged();
        }




        



        public void OnNavigatedTo()
        {
            
        }

        public void OnNavigatedFrom()
        {
            
        }

        
    }
}
