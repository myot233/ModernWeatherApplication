using System.Collections.ObjectModel;
using System.Drawing;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;

using ModernWeatherApplication.Properties;
using Wpf.Ui.Controls;
using System.Windows;
using System.Windows.Controls;
using ModernWeatherApplication.Service;
using Wpf.Ui.Appearance;
namespace ModernWeatherApplication.ViewModel
{
    public partial class SettingViewModel: ObservableObject, INavigationAware
    {
        public int Location => _jroot[FirstSelected]![SecondSelected]![LastSelected]!.Value<int>();

        public event Action onLocationChanged;

        [ObservableProperty]
        private ApplicationTheme _currentApplicationTheme = ApplicationTheme.Unknown;
        [ObservableProperty] private string _firstSelected = "北京市";
        [ObservableProperty] private string _secondSelected = "北京市";
        [ObservableProperty] private string _lastSelected = "北京";
        [ObservableProperty] private List<string> _firstPlace;
        [ObservableProperty] private List<string> _secondPlace;
        [ObservableProperty] private List<string> _lastPlace;
        
        

        private readonly JObject _jroot = JObject.Parse(System.Text.Encoding.UTF8.GetString(Resources.location));
        public SettingViewModel()
        {
            CurrentApplicationTheme = ApplicationThemeManager.GetAppTheme();
            onLocationChanged += () => { };
            FirstPlace = _jroot.Properties().Select(x=>x.Name).ToList();
            FirstSelected = FirstPlace[0];

        }
        partial void OnCurrentApplicationThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
        {
            ApplicationThemeManager.Apply(newValue);
        }
       
        public void OnFirstSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)

        {
            SecondPlace = (_jroot[FirstSelected] as JObject)?.Properties().Select(x => x.Name).ToList() ?? new List<string>();
            SecondSelected = SecondPlace[0];
            

        }


        public void OnSecondSelected(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (!_jroot.ContainsKey(FirstSelected)) return;
            if (!(_jroot[FirstSelected] as JObject)!.ContainsKey(SecondSelected ?? "")) return;
            LastPlace = (_jroot[FirstSelected]?[SecondSelected] as JObject)?.Properties().Select(x => x.Name)
                .ToList() ?? new List<string>();
            LastSelected = LastPlace[0];
            
        }

        public void OnLastSeleceted(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (!_jroot.ContainsKey(FirstSelected)) return;
            if (!(_jroot[FirstSelected] as JObject)!.ContainsKey(SecondSelected ?? "")) return;
            if (SecondSelected != null && !(_jroot[FirstSelected]?[SecondSelected] as JObject)!.ContainsKey(LastSelected ?? "")) return;
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
