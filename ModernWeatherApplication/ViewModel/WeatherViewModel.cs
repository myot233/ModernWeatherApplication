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
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using System.Windows.Ink;
using System.Windows.Threading;

namespace ModernWeatherApplication.ViewModel;

public partial class WeatherViewModel : ObservableObject,INavigationAware
{
    [ObservableProperty] private ObservableCollection<WeatherModel> _items = new();
    [ObservableProperty] private WeatherModel _selected = null!;
    [ObservableProperty] private Visibility _lstVisibility;
    [ObservableProperty] private Visibility _loadingVisibility;
    [ObservableProperty] private ISeries[] _series = null!;
    [ObservableProperty] private Axis[] _xAxes = null!;
    [ObservableProperty] private Axis[] _yAxes = null!;
    [ObservableProperty] private IEnumerable<ISeries> _pieSeries = null!;
    [ObservableProperty] private WeatherIndexModel _selectedIndex = null!;
    [ObservableProperty] ObservableCollection<WeatherIndexModel> _indexSeries = new();
    [ObservableProperty] private SolidColorPaint _legendTextPaint = null!;
    [ObservableProperty] private Setting _setting;
    [ObservableProperty] private AirPollution airPollution;
    public WeatherViewModel(ApiService service, SettingViewModel viewModel,Setting setting)
    {

        LstVisibility = Visibility.Hidden;
        LoadingVisibility = Visibility.Visible;
        Setting = setting;
        LiveCharts.Configure(config => config.HasGlobalSKTypeface(SKFontManager.Default.MatchCharacter('汉')));
        viewModel.onLocationChanged += async () =>
        {
            
            Items.Clear();
            await InitAllAsync(service,viewModel);
            ChangeControlsByTheme(ApplicationThemeManager.GetAppTheme());
        };
        ApplicationThemeManager.Changed += (sender, _) => { ChangeControlsByTheme(sender); };

        var t  = InitAllAsync(service, viewModel);
        
    }

    private void ChangeControlsByTheme(ApplicationTheme sender)
    {
        switch (sender)
        {
            case ApplicationTheme.Light or ApplicationTheme.HighContrast:
                LegendTextPaint = new SolidColorPaint(SKColors.Black);
                foreach (var xAx in XAxes)
                {
                    xAx.LabelsPaint = LegendTextPaint;
                }
                foreach (var series in Series)
                {
                    ((series as LineSeries<double>)!).DataLabelsPaint = new SolidColorPaint(SKColors.DarkGray);
                }
                break;
            case ApplicationTheme.Dark:
                LegendTextPaint = new SolidColorPaint(SKColors.White);
                foreach (var xAx in XAxes)
                {
                    xAx.LabelsPaint = LegendTextPaint;
                        
                }
                foreach (var series in Series)
                {
                    ((series as LineSeries<double>)!).DataLabelsPaint = new SolidColorPaint(SKColors.LightGray);
                }
                break;
                
        }
    }

    public async Task InitAirPollution(ApiService service, SettingViewModel viewModel)
    {
        AirPollution = await service.FetchAirPollutionNow(viewModel.Location);
    }

    public async Task InitAllAsync(ApiService service, SettingViewModel viewModel)
    {
        
        var retryCount = 10;

        while (retryCount > 0)
        {
            try
            {
               /*
                await Init24HourItem(service,viewModel);
                await InitSevenDayItem(service,viewModel);
                await InitWeatherIndex(service, viewModel); 
               */
               await Task.WhenAll(
                   Init24HourItem(service, viewModel), 
                   InitAirPollution(service,viewModel),
                   InitSevenDayItem(service, viewModel),
                   InitWeatherIndex(service, viewModel)
                   );
                   ChangeControlsByTheme(ApplicationThemeManager.GetAppTheme());
                break;
            }
            catch (HttpRequestException)
            {
                retryCount--;
                service.Refersh();
            }


        }
    }


    public async Task InitWeatherIndex(ApiService service, SettingViewModel viewModel)
    {
        var lst = await service.FetchWeatherIndex(viewModel.Location);
        foreach (var weatherIndexModel in lst.Select(x => new WeatherIndexModel(x)))
        {
            IndexSeries.Add(weatherIndexModel);
        }

        SelectedIndex = IndexSeries[0];
        


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
                DataLabelsSize = 17,
                DataLabelsPaint = new SolidColorPaint(SKColors.DarkGray),
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                DataLabelsFormatter = (point) => point.Coordinate.PrimaryValue.ToString(CultureInfo.InvariantCulture) + "℃",
                Values = lst.Select(x => double.Parse(x.temp)),
                Name = "温度",
                Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 0 },
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