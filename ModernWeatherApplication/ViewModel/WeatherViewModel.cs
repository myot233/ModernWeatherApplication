using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using ModernWeatherApplication.Service;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Windows;
using LiveChartsCore.SkiaSharpView.Painting;
using ModernWeatherApplication.Model;
using SkiaSharp;
using Wpf.Ui.Controls;

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

    [ObservableProperty] private Axis[] _xAxes;

    [ObservableProperty] private Axis[] _yAxes;

    [ObservableProperty] private IEnumerable<ISeries> _pieSeries;

    public WeatherViewModel(ApiService service, SettingViewModel viewModel)
    {

        LstVisibility = Visibility.Hidden;
        LoadingVisibility = Visibility.Visible;
        LiveCharts.Configure(config => config.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')));
        viewModel.onLocationChanged += async () =>
        {
            Console.WriteLine("location changed");
            Items.Clear();
            await InitAllAsync(service,viewModel);
        };
        InitAllAsync(service, viewModel);
    }

    public async Task InitAllAsync(ApiService service, SettingViewModel viewModel)
    {
        
        var retryCount = 10;

        while (retryCount > 0)
        {
            try
            {
                await Init24HourItem(service,viewModel);
                await InitSevenDayItem(service,viewModel);
                break;
            }
            catch (HttpRequestException)
            {
                retryCount--;
            }


        }
    }

   


    public async Task InitSevenDayItem(ApiService service, SettingViewModel viewModel)
    {
        
        var lst = await service.FetchWeatherDataSevenDay(viewModel.Location);
        lst.ForEach((x) => { Items.Add(new WeatherModel(x)); });
        Selected = Items[0];
        LstVisibility = Visibility.Visible;
        LoadingVisibility = Visibility.Hidden;
        
    }

    public async Task Init24HourItem(ApiService service, SettingViewModel viewModel)
    {
        var lst = await service.FetchWeatherDataPerHour(viewModel.Location);
        Series = new ISeries[]
        {
            new LineSeries<double>
            {
                DataLabelsSize = 15,
                DataLabelsPaint = new SolidColorPaint(SKColors.DarkGray),
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                DataLabelsFormatter = (point) => point.Coordinate.PrimaryValue.ToString(CultureInfo.InvariantCulture) + "℃",
                Values = lst.Select(x => double.Parse(x.temp)),
                Name = "温度",
                Fill = new SolidColorPaint(SKColors.CornflowerBlue),
                LineSmoothness = 1,
                
                GeometrySize = 0,
            }
        };
        XAxes = new Axis[]
        {
            new()
            {
                
                ShowSeparatorLines = false,
                Labels = lst.Select(x =>  DateTime.ParseExact(x.fxTime,"yyyy-MM-ddTHH:mmzzz",null).ToString("t")).ToList()
            }
        };
        YAxes = new Axis[]
        {
            new()
            {
                Labels = null,
                ShowSeparatorLines = false,
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