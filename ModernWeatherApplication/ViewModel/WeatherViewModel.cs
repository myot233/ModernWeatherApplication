using System.IO;
using System.Resources;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using ModernWeatherApplication.Properties;
using ModernWeatherApplication.Service;
using SkiaSharp;
using Wpf.Ui.Controls;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace ModernWeatherApplication.ViewModel;

public partial class WeatherViewModel : ObservableObject,INavigationAware
{
    [ObservableProperty] private ISeries[] _series = { new LineSeries<double> { Values = new double[]{ 114.2   } } };

    [ObservableProperty] private Axis[] _xAxes = null!;

    [ObservableProperty] private int _location = 101210106;

    [ObservableProperty] Visibility _visible = Visibility.Hidden;
    [ObservableProperty] private Visibility _loadingVisibility = Visibility.Visible;
    public Paint LabelPaint { get; set; } = null!;
   

    public WeatherViewModel(ApiService service)
    {
        var result = UpgradeUi(service);
        

    }

    public async Task UpgradeUi(ApiService service)
    {
        var result = await service.FetchWeatherData(Location);
        
        XAxes = new[]
        {
            new Axis
            {
                LabelsRotation = 15,
                LabelsPaint =  new SolidColorPaint
                {
                    Color = SKColors.DarkSlateGray,
                    SKTypeface = SKFontManager.Default.MatchCharacter('汉')
                },
                ShowSeparatorLines = false,
                Labels = result.Select((x) => x.fxDate).ToList()
            }
        };
        Series = new ISeries[] 
        { 
            new LineSeries<double>
            {
                Values = result.Select((x) => double.Parse(x.tempMax)),
                Fill = null
            },
            new LineSeries<double>
            {
                Values = result.Select((x) => double.Parse(x.tempMin)),

                Fill = null
            }

        };
        Visible = Visibility.Visible;
        LoadingVisibility = Visibility.Hidden;
    }

    public void OnNavigatedTo()
    {
            
    }

    public void OnNavigatedFrom()
    {
            
    }
    

}