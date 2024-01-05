using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Extensions.Logging;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace ModernWeatherApplication.Service
{
    public class ApiService
    {
        private readonly ILogger _logger = App.GetService<ILogger<ApiService>>();
        public ApiService(ISnackbarService service)
        {
            
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

        private  HttpClient _client;


        public void Refersh()
        {
            _client = new HttpClient(handler);
        }

        public async Task<List<DayWeatherData>?> FetchWeatherDataSevenDay(int location)
        {
            try
            {

                var result = await _client
                    .GetAsync($"{SevenDayUrl}?location={location}&key={Key}&lang={Lang}&unit={Unit}")
                    .WaitAsync(TimeSpan.FromSeconds(20));
                var jobj = JsonSerializer.Deserialize<JsonObject>(await result.Content.ReadAsStringAsync());
                    if ((jobj["code"] ?? throw new InvalidOperationException("error code")).GetValue<string>() != "200")
                    {
                        throw new InvalidOperationException($"operation failed {jobj}");
                    }
                    _logger.LogInformation(jobj.ToString());
                    return jobj["daily"]!.Deserialize<List<DayWeatherData>>();
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception.StackTrace);
                snackbarService.Show("错误",exception.Message, ControlAppearance.Caution, timeout: TimeSpan.FromSeconds(10));
                throw;

            }
        }

        

        public async Task<List<HourWeatherData>?> FetchWeatherDataPerHour(int location)
        {
            try{
                var result = await _client.GetAsync($"{PerHourUrl}?location={location}&key={Key}&lang={Lang}&unit={Unit}");
                var jobj = JsonSerializer.Deserialize<JsonObject>(await result.Content.ReadAsStringAsync());
                if ((jobj["code"] ?? throw new InvalidOperationException("error code")).GetValue<string>() != "200")
                {
                    throw new InvalidOperationException($"operation failed {jobj}");
                }
                _logger.LogInformation(jobj.ToString());
                return jobj["hourly"]!.Deserialize<List<HourWeatherData>>();
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception.Message);
                _logger.LogError(exception.InnerException?.Message);
                _logger.LogError(exception.StackTrace);
                snackbarService.Show("错误", exception.Message, ControlAppearance.Caution,timeout:TimeSpan.FromSeconds(10));
                throw;

            }
        }

        public async Task<List<WeatherIndex>?> FetchWeatherIndex(int location)
        {
            try
            {
                var result = await _client.GetAsync($"{WeatherIndexUrl}?location={location}&key={Key}&type=0");
                var jobj = JsonSerializer.Deserialize<JsonObject>(await result.Content.ReadAsStringAsync());
                if ((jobj["code"] ?? throw new InvalidOperationException("error code")).GetValue<string>() != "200")
                {
                    throw new InvalidOperationException($"operation failed {jobj}");
                }
                _logger.LogInformation(jobj.ToString());
                return jobj["daily"]!.Deserialize<List<WeatherIndex>>();
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception.Message);
                _logger.LogError(exception.InnerException?.Message);
                _logger.LogError(exception.StackTrace);
                snackbarService.Show("错误", exception.Message, ControlAppearance.Caution, timeout: TimeSpan.FromSeconds(10));
                throw;

            }
        }

        public async Task<AirPollution?> FetchAirPollutionNow(int location)
        {
            try{
                var result = await _client.GetAsync($"{AirPollutionUrl}?location={location}&key={Key}&lang={Lang}");
                var jobj = JsonSerializer.Deserialize<JsonObject>(await result.Content.ReadAsStringAsync());
                if ((jobj["code"] ?? throw new InvalidOperationException("error code")).GetValue<string>() != "200")
                {
                    throw new InvalidOperationException($"operation failed {jobj}");
                }
                _logger.LogInformation(jobj.ToString());
                return jobj["now"]!.Deserialize<AirPollution>();
            }
            catch (HttpRequestException exception)
            {
                _logger.LogError(exception.Message);
                _logger.LogError(exception.InnerException?.Message);
                _logger.LogError(exception.StackTrace);
                snackbarService.Show("错误", exception.Message,ControlAppearance.Caution, timeout: TimeSpan.FromSeconds(10));
                throw;

            }
        }
        





    }
}
