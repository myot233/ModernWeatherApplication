using ModernWeatherApplication.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui.Controls;
using ModernWeatherApplication.Service;

namespace ModernWeatherApplication.Views
{
    /// <summary>
    /// MainViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class MainViewPage : INavigableView<MainViewPageModel>
    {
        
        public MainViewPage(MainViewPageModel viewModel)
        {
            
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
        }

        public MainViewPageModel ViewModel { get; set; }

        
    }
}
