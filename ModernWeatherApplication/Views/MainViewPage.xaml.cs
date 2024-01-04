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
using CommunityToolkit.Mvvm.ComponentModel;
using Wpf.Ui.Controls;

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
            InitializeComponent();
        }

        public MainViewPageModel ViewModel { get; set; }
    }

    public class MainViewPageModel:ObservableObject,INavigationAware
    {
        
        
        
        public void OnNavigatedTo()
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedFrom()
        {
            //throw new NotImplementedException();
        }
    }
}
