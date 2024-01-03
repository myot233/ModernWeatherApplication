using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace ModernWeatherApplication.Service
{
    public class ApiService
    {
        private ILogger _logger;
        public ApiService(ISnackbarService service,LoggerService loggerService)
        {
            _logger = loggerService.CreateLogger(nameof(ApiService));
            _client = new HttpClient(handler);
            snackbarService = service;
            _client.Timeout = TimeSpan.FromSeconds(20);
        }

        private const string SevenDayUrl = "https://devapi.qweather.com/v7/weather/7d";
        private const string PerHourUrl = "https://devapi.qweather.com/v7/weather/24h";
        private const string AirPollutionUrl = "https://devapi.qweather.com/v7/air/now";
        private const string WeatherIndexUrl = "https://devapi.qweather.com/v7/indices/1d";
        private ISnackbarService snackbarService;
        private const string Key = "9604612732b54b618f4c6f82e5b598db";
        private const string Lang = "zh-hans";
        private const string Unit = "m";

        HttpClientHandler handler = new()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        private readonly HttpClient _client;

        public async Task<List<DayWeatherData>> FetchWeatherDataSevenDay(int location)
        {
            try
            {

                var result = await _client
                    .GetAsync($"{SevenDayUrl}?location={location}&key={Key}&lang={Lang}&unit={Unit}")
                    .WaitAsync(TimeSpan.FromSeconds(20));
                    var jobj = JObject.Parse(await result.Content.ReadAsStringAsync());
                    if ((jobj["code"] ?? throw new InvalidOperationException("error code")).Value<int>() != 200)
                    {
                        throw new InvalidOperationException($"operation failed {jobj}");
                    }
                    _logger.LogInformation(jobj.ToString());
                    return jobj["daily"]!.Children().ToList().Select((x) =>
                        x.ToObject<DayWeatherData>()!).ToList();
                
            }
            catch (HttpRequestException exception)
            {
                snackbarService.Show("错误",exception.Message, ControlAppearance.Caution, timeout: TimeSpan.FromSeconds(10));
                throw;

            }
        }

        

        public async Task<List<HourWeatherData>> FetchWeatherDataPerHour(int location)
        {
            try{
                var result = await _client.GetAsync($"{PerHourUrl}?location={location}&key={Key}&lang={Lang}&unit={Unit}");
                var jobj = JObject.Parse(await result.Content.ReadAsStringAsync());
                if ((jobj["code"] ?? throw new InvalidOperationException("error code")).Value<int>() != 200)
                {
                    throw new InvalidOperationException($"operation failed {jobj}");
                }
                _logger.LogInformation(jobj.ToString());
                return jobj["hourly"]!.Children().ToList().Select((x) =>
                    x.ToObject<HourWeatherData>()!).ToList();
            }
            catch (HttpRequestException exception)
            {
                snackbarService.Show("错误", exception.Message, ControlAppearance.Caution,timeout:TimeSpan.FromSeconds(10));
                throw;

            }
        }

        public async Task<List<WeatherIndex>> FetchWeatherIndex(int location)
        {
            try
            {
                var result = await _client.GetAsync($"{WeatherIndexUrl}?location={location}&key={Key}&type=0");
                var jobj = JObject.Parse(await result.Content.ReadAsStringAsync());
                if ((jobj["code"] ?? throw new InvalidOperationException("error code")).Value<int>() != 200)
                {
                    throw new InvalidOperationException($"operation failed {jobj}");
                }
                _logger.LogInformation(jobj.ToString());
                return jobj["daily"]!.Children().ToList().Select((x) =>
                    x.ToObject<WeatherIndex>()!).ToList();
            }
            catch (HttpRequestException exception)
            {
                snackbarService.Show("错误", exception.Message, ControlAppearance.Caution, timeout: TimeSpan.FromSeconds(10));
                throw;

            }
        }

        public async Task<AirPollution> FetchAirPollutionNow(int location)
        {
            try{
                var result = await _client.GetAsync($"{AirPollutionUrl}?location={location}&key={Key}&lang={Lang}");
                var jobj = JObject.Parse(await result.Content.ReadAsStringAsync());
                if ((jobj["code"] ?? throw new InvalidOperationException("error code")).Value<int>() != 200)
                {
                    throw new InvalidOperationException($"operation failed {jobj}");
                }
                _logger.LogInformation(jobj.ToString());
                return jobj["now"]!.ToObject<AirPollution>()!;
            }
            catch (HttpRequestException exception)
            {
                snackbarService.Show("错误", exception.Message,ControlAppearance.Caution, timeout: TimeSpan.FromSeconds(10));
                throw;

            }
        }
        





    }
}
