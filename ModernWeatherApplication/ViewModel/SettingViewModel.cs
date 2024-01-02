using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json.Linq;

using ModernWeatherApplication.Properties;
using Wpf.Ui.Controls;
using System.Windows;
using System.Windows.Controls;
using ModernWeatherApplication.Service;

namespace ModernWeatherApplication.ViewModel
{
    public partial class SettingViewModel: ObservableObject, INavigationAware
    {
        public int Location => _jroot[FirstSelected]![SecondSelected]![LastSelected]!.Value<int>();

        public event Action onLocationChanged; 

        [ObservableProperty] private string _firstSelected;
        [ObservableProperty] private string _secondSelected;
        [ObservableProperty] private string _lastSelected;
        [ObservableProperty] private List<string> _firstPlace;
        [ObservableProperty] private List<string> _secondPlace;
        [ObservableProperty] private List<string> _lastPlace;
        

        private readonly JObject _jroot = JObject.Parse(System.Text.Encoding.UTF8.GetString(Resources.location));
        public SettingViewModel()
        {
            onLocationChanged += () => { };
            FirstPlace = _jroot.Properties().Select(x=>x.Name).ToList();
            FirstSelected = FirstPlace[0];

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
