using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using ModernWeatherApplication.Model;
using ModernWeatherApplication.Service;
using System.Collections.ObjectModel;
using System.Windows;
using ModernWeatherApplication.Properties;
using Wpf.Ui.Controls;
using MessageBox = System.Windows.MessageBox;

namespace ModernWeatherApplication.ViewModel;

public partial class WeatherViewModel : ObservableObject,INavigationAware
{
    [ObservableProperty]
    private ObservableCollection<WeatherModel> _items = new();

    [ObservableProperty] 
    private WeatherModel _selected;

    [ObservableProperty] 
    private Visibility _lstVisibility;

    [ObservableProperty] 
    private Visibility _loadingVisibility;

    [ObservableProperty] private ISeries[] _series;

    public WeatherViewModel(ApiService service)
    {
        LstVisibility = Visibility.Hidden;
        LoadingVisibility = Visibility.Visible;
        var t = Task.WhenAll(Init24HourItem(service), InitSevenDayItem(service));


    }

    public async Task InitSevenDayItem(ApiService service)
    {
        
        var lst = await service.FetchWeatherDataSevenDay(101210106);
        lst.ForEach((x) => { Items.Add(new WeatherModel(x)); });
        Selected = Items[0];
        (LstVisibility, LoadingVisibility) = (LoadingVisibility, LstVisibility);
    }

    public async Task Init24HourItem(ApiService service)
    {
        var lst = await service.FetchWeatherDataPerHour(101210106);
        Series = new ISeries[]
        {
            new LineSeries<double>
            {
                Values = lst.Select(x => double.Parse(x.temp)),
                Fill = null
            }
        };
    }



    

    public void OnNavigatedTo()
    {
            
    }

    public void OnNavigatedFrom()
    {
            
    }
    

}