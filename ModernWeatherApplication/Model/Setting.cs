using CommunityToolkit.Mvvm.ComponentModel;
using System.IO;
using System.Text.Json;
using Wpf.Ui.Appearance;

namespace ModernWeatherApplication.Model
{
    public sealed partial class Setting: ObservableObject
    {
        
        [ObservableProperty] private string _province;
        [ObservableProperty] private string _city;
        [ObservableProperty] private string _region;
        [ObservableProperty] private ApplicationTheme _theme;


        public Setting(string province, string city, string region, ApplicationTheme theme)
        {
            Province = province;
            City = city;
            Region = region;
            Theme = theme;
        }
        public static Setting? LoadFromFile()
            => JsonSerializer.Deserialize<Setting>(File.ReadAllText("setting.json"));

        public static Setting LoadFromFIleOrDefault(Setting @default)
        {
            if (File.Exists("setting.json"))
            {
                var obj= LoadFromFile()!;
                Console.WriteLine($"{obj.Province} {obj.City} {obj.Region}");
                return LoadFromFile()!;
            }
            return @default;

        }

        public void SaveToFile()
            => File.WriteAllText("setting.json", JsonSerializer.Serialize(this));

        public Task SaveToFileAsync() 
            => File.WriteAllTextAsync("setting.json", JsonSerializer.Serialize(this));

        public static async Task<Setting?> LoadFromFileAsync()
            => JsonSerializer.Deserialize<Setting>(await File.ReadAllTextAsync("setting.json"));


        partial void OnThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
        {
            ApplicationThemeManager.Apply(newValue);
            SaveToFile();
        }
    }
}
