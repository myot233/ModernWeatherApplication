using System.Windows;
using System.Windows.Controls;
using ModernWeatherApplication.ViewModel;
using Wpf.Ui.Controls;

namespace ModernWeatherApplication.Views
{
    /// <summary>
    /// SettingViewPage.xaml 的交互逻辑
    /// </summary>
    public partial class SettingViewPage : INavigableView<SettingViewModel>
    {

        public SettingViewPage(SettingViewModel vm)
        {
            InitializeComponent();
            ViewModel = vm;
            DataContext = this;
            
        }

        public SettingViewModel ViewModel { get; set; }


        public void Selector_OnSelected(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.OnFirstSelected(sender,e);
        }

        private void Selector1_OnSelected(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.OnSecondSelected(sender,e);
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.OnLastSeleceted(sender,e);
        }
    }

    


}
