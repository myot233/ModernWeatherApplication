using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ModernWeatherApplication.Service
{
    public class ApiService
    {
        public ApiService()
        {

        }

        private const string url = "https://devapi.qweather.com/v7/weather/7d";
        private const string key = "9604612732b54b618f4c6f82e5b598db";
        private const string lang = "zh-hans";
        private const string unit = "m";
        HttpClientHandler handler = new()
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };

        public async Task<List<WeatherData>> FetchWeatherData(int location)
        {
            var client = new HttpClient(handler);
            var result = await client.GetAsync($"{url}?location={location}&key={key}&lang={lang}&unit={unit}");
            var jobj = JObject.Parse(await result.Content.ReadAsStringAsync());
            if ((jobj["code"] ?? throw new InvalidOperationException("error code")).Value<int>() != 200)
            {
                throw new InvalidOperationException($"operation failed {jobj}");
            }

            return jobj["daily"]!.Children().ToList().Select((x) => 
                x.ToObject<WeatherData>()!).ToList();


        }





    }



    public class WeatherData
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
