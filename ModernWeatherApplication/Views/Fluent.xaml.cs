using Wpf.Ui.Contracts;

namespace ModernWeatherApplication.Views
{
    /// <summary>
    /// Fluent.xaml 的交互逻辑
    /// </summary>
    public partial class FluentMainWindow
    {
        public ViewModel.MainViewModel ViewModel { get; set; }
        public FluentMainWindow(
            ViewModel.MainViewModel viewModel, 
            INavigationService navigationService,
            ISnackbarService snackbarService,
            IContentDialogService contentDialogService
            )
        {
            
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
            navigationService.SetNavigationControl(nav);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
        }
    }
}
