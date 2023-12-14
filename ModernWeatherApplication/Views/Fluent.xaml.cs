using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wpf.Ui.Controls;

namespace ModernWeatherApplication.Views
{
    /// <summary>
    /// Fluent.xaml 的交互逻辑
    /// </summary>
    public partial class FluentMainWindow:FluentWindow
    {
        public MainViewModel ViewModel { get; set; }
        public FluentMainWindow(MainViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }
    }
}
