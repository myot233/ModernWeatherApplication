using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWeatherApplication
{
    public partial class MainViewModel: ObservableObject
    {
        [ObservableProperty]
        public string applicationTitle = "天气";

    }
}
