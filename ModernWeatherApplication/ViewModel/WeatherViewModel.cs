using System.Collections.ObjectModel;
using System.IO;
using System.Resources;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using ModernWeatherApplication.Model;
using ModernWeatherApplication.Properties;
using ModernWeatherApplication.Service;
using SkiaSharp;
using Wpf.Ui.Controls;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace ModernWeatherApplication.ViewModel;

public partial class WeatherViewModel : ObservableObject,INavigationAware
{
    [ObservableProperty]
    private ObservableCollection<WeatherModel> _items = new();

    public WeatherViewModel(ApiService service)
    {
       var t =  InitItem(service);


    }

    public async Task InitItem(ApiService service)
    {
        
        var lst = await service.FetchWeatherData(101210106);
        lst.ForEach((x) => { Items.Add(new WeatherModel(x)); });
        
    }

    

    public void OnNavigatedTo()
    {
            
    }

    public void OnNavigatedFrom()
    {
            
    }
    

}