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
using CommunityToolkit.Mvvm.Input;
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

    public partial class MainViewPageModel : ObservableObject, INavigationAware
    {

        public MainViewPageModel()
        {

        }

        [RelayCommand]
        private void SourceCodeButton()
        {
            var target = "https://github.com/myot233/ModernWeatherApplication";
            try
            {
                System.Diagnostics.Process.Start("explorer.exe",target);

            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    System.Windows.MessageBox.Show(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                System.Windows.MessageBox.Show(other.Message);
            }
        }
        
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
