using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace ModernWeatherApplication.Controls
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ModernWeatherApplication"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:ModernWeatherApplication;assembly=ModernWeatherApplication"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:WeatherControl/>
    ///
    /// </summary>
    ///
    
    public class WeatherControl : Control
    {
        public static readonly DependencyProperty MaxTempProperty = DependencyProperty.Register(
            nameof(MaxTemp),
            typeof(string),
            typeof(WeatherControl),
            new PropertyMetadata(null)
        );

        public static readonly DependencyProperty MinTempProperty = DependencyProperty.Register(
            nameof(MinTemp),
            typeof(string),
            typeof(WeatherControl),
            new PropertyMetadata(null)
        );

        public static readonly DependencyProperty MaxImageProperty = DependencyProperty.Register(
            nameof(MaxImage),
            typeof(ImageSource),
            typeof(WeatherControl),
            new PropertyMetadata(null)
        );
        public static readonly DependencyProperty MinImageProperty = DependencyProperty.Register(
            nameof(MinImage),
            typeof(ImageSource),
            typeof(WeatherControl),
            new PropertyMetadata(null)
        );

        public ImageSource MinImage
        {
            get => (ImageSource)GetValue(MinImageProperty);
            set => SetValue(MinImageProperty, value);
        }

        public ImageSource MaxImage
        {
            get => (ImageSource)GetValue(MaxImageProperty);
            set => SetValue(MaxImageProperty, value);
        }

        public string MaxTemp
        {
            get => (string)GetValue(MaxTempProperty);
            set => SetValue(MaxTempProperty, value);
        }

        public string MinTemp
        {
            get => (string)GetValue(MinTempProperty);
            set => SetValue(MinTempProperty, value);
        }
        static WeatherControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WeatherControl), new FrameworkPropertyMetadata(typeof(WeatherControl)));
        }
    }
}
