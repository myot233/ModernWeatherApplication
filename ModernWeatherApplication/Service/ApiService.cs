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
using Wpf.Ui.Contracts;
using Wpf.Ui.Controls;

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

    public class AirPollution
    {
        public string pubTime { get; set; }
        public string aqi { get; set; }
        public string level { get; set; }
        public string category { get; set; }
        public string primary { get; set; }
        public string pm10 { get; set; }
        public string pm2p5 { get; set; }
        public string no2 { get; set; }
        public string so2 { get; set; }
        public string co { get; set; }
        public string o3 { get; set; }
    }

    public class HourWeatherData
    {
        public string fxTime { get; set; }
        public string temp { get; set; }
        public string icon { get; set; }
        public string text { get; set; }
        public string wind360 { get; set; }
        public string windDir { get; set; }
        public string windScale { get; set; }
        public string windSpeed { get; set; }
        public string humidity { get; set; }
        public string pop { get; set; }
        public string precip { get; set; }
        public string pressure { get; set; }
        public string cloud { get; set; }
        public string dew { get; set; }
    }


    public class DayWeatherData
    {
        /// <summary>
        /// 
        /// </summary>
        public string fxDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sunrise { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sunset { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string moonrise { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string moonset { get; set; }
        /// <summary>
        /// 峨眉月
        /// </summary>
        public string moonPhase { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string moonPhaseIcon { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tempMax { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string tempMin { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iconDay { get; set; }
        /// <summary>
        /// 阴
        /// </summary>
        public string textDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string iconNight { get; set; }
        /// <summary>
        /// 多云
        /// </summary>
        public string textNight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wind360Day { get; set; }
        /// <summary>
        /// 西北风
        /// </summary>
        public string windDirDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string windScaleDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string windSpeedDay { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string wind360Night { get; set; }
        /// <summary>
        /// 西北风
        /// </summary>
        public string windDirNight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string windScaleNight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string windSpeedNight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string humidity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string precip { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string pressure { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vis { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cloud { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string uvIndex { get; set; }
    }

}
