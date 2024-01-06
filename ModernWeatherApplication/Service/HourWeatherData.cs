namespace ModernWeatherApplication.Service;

public class HourWeatherData
{
    public HourWeatherData(string fxTime, string temp, string icon, string text, string wind360, string windDir, string windScale, string windSpeed, string humidity, string pop, string precip, string pressure, string cloud, string dew)
    {
        this.fxTime = fxTime;
        this.temp = temp;
        this.icon = icon;
        this.text = text;
        this.wind360 = wind360;
        this.windDir = windDir;
        this.windScale = windScale;
        this.windSpeed = windSpeed;
        this.humidity = humidity;
        this.pop = pop;
        this.precip = precip;
        this.pressure = pressure;
        this.cloud = cloud;
        this.dew = dew;
    }
    
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