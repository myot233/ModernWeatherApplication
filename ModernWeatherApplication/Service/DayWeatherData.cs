namespace ModernWeatherApplication.Service;

public class DayWeatherData
{
    public DayWeatherData(string fxDate, string sunrise, string sunset, string moonrise, string moonset, string moonPhase, string moonPhaseIcon, string tempMax, string tempMin, string iconDay, string textDay, string iconNight, string textNight, string wind360Day, string windDirDay, string windScaleDay, string windSpeedDay, string wind360Night, string windDirNight, string windScaleNight, string windSpeedNight, string humidity, string precip, string pressure, string vis, string cloud, string uvIndex)
    {
        this.fxDate = fxDate;
        this.sunrise = sunrise;
        this.sunset = sunset;
        this.moonrise = moonrise;
        this.moonset = moonset;
        this.moonPhase = moonPhase;
        this.moonPhaseIcon = moonPhaseIcon;
        this.tempMax = tempMax;
        this.tempMin = tempMin;
        this.iconDay = iconDay;
        this.textDay = textDay;
        this.iconNight = iconNight;
        this.textNight = textNight;
        this.wind360Day = wind360Day;
        this.windDirDay = windDirDay;
        this.windScaleDay = windScaleDay;
        this.windSpeedDay = windSpeedDay;
        this.wind360Night = wind360Night;
        this.windDirNight = windDirNight;
        this.windScaleNight = windScaleNight;
        this.windSpeedNight = windSpeedNight;
        this.humidity = humidity;
        this.precip = precip;
        this.pressure = pressure;
        this.vis = vis;
        this.cloud = cloud;
        this.uvIndex = uvIndex;
    }

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