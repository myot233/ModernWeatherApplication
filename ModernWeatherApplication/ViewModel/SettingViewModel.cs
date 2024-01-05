using System.Text.Json;
using System.Text.Json.Nodes;
using CommunityToolkit.Mvvm.ComponentModel;
using ModernWeatherApplication.Properties;
using Wpf.Ui.Controls;
using System.Windows.Controls;
using Wpf.Ui.Appearance;
namespace ModernWeatherApplication.ViewModel
{
    public partial class SettingViewModel: ObservableObject, INavigationAware
    {
        public int Location => int.Parse(_jroot[FirstSelected]![SecondSelected]![LastSelected]!.GetValue<string>());

        public event Action onLocationChanged;

        [ObservableProperty]
        private ApplicationTheme _currentApplicationTheme = ApplicationTheme.Unknown;
        [ObservableProperty] private string _firstSelected = "北京市";
        [ObservableProperty] private string _secondSelected = "北京市";
        [ObservableProperty] private string _lastSelected = "北京";
        [ObservableProperty] private List<string> _firstPlace;
        [ObservableProperty] private List<string> _secondPlace;
        [ObservableProperty] private List<string> _lastPlace;
        
        

        private readonly JsonObject _jroot =  JsonSerializer.Deserialize<JsonObject>(System.Text.Encoding.UTF8.GetString(Resources.location));
        public SettingViewModel()
        {
            CurrentApplicationTheme = ApplicationThemeManager.GetAppTheme();
            onLocationChanged += () => { };
            FirstPlace = _jroot.Select(x => x.Key).ToList();
            FirstSelected = FirstPlace[0];

        }
        partial void OnCurrentApplicationThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
        {
            ApplicationThemeManager.Apply(newValue);
        }
       
        public void OnFirstSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)

        {
            SecondPlace = (_jroot[FirstSelected] as JsonObject)?.Select(x => x.Key).ToList() ?? new List<string>();
            SecondSelected = SecondPlace[0];
            

        }


        public void OnSecondSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (!_jroot.ContainsKey(FirstSelected)) return;
            if (!(_jroot[FirstSelected] as JsonObject)!.ContainsKey(SecondSelected ?? "")) return;
            LastPlace = (_jroot[FirstSelected]?[SecondSelected] as JsonObject)?.Select(x => x.Key)
                .ToList() ?? new List<string>();
            LastSelected = LastPlace[0];
            
        }

        public void OnLastSeleceted(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (!_jroot.ContainsKey(FirstSelected)) return;
            if (!(_jroot[FirstSelected] as JsonObject)!.ContainsKey(SecondSelected ?? "")) return;
            if (SecondSelected != null && !(_jroot[FirstSelected]?[SecondSelected] as JsonObject)!.ContainsKey(LastSelected ?? "")) return;
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
