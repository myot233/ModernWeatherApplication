using Wpf.Ui;
using Wpf.Ui.Controls;
using Wpf.Ui.Appearance;

namespace ModernWeatherApplication.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class FluentMainWindow: INavigationWindow
    {
        public ViewModel.MainViewModel ViewModel { get; set; }
        public FluentMainWindow(
            ViewModel.MainViewModel viewModel, 
            INavigationService navigationService,
            ISnackbarService snackbarService,
            IPageService pageService
            )
        {
            SystemThemeWatcher.Watch(this);
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
            SetPageService(pageService);
            navigationService.SetNavigationControl(Nav);
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            Loaded += (_, _) => Nav.Navigate(typeof(MainViewPage));
        }

        #region Implementation of NavigationWindow

        public INavigationView GetNavigation() => Nav;

        public bool Navigate(Type pageType) => Nav.Navigate(pageType);

        public void SetServiceProvider(IServiceProvider serviceProvider) => Nav.SetServiceProvider(serviceProvider);

        public void SetPageService(IPageService pageService) => Nav.SetPageService(pageService);

        public void ShowWindow() => Show();

        public void CloseWindow() => Close();

        #endregion

    }
}
