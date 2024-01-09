using Microsoft.Extensions.Hosting;
using ModernWeatherApplication.Views;
using System.Windows;
using ModernWeatherApplication.Model;
using Wpf.Ui;
using Wpf.Ui.Appearance;

namespace ModernWeatherApplication.Service
{
    public class HostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private INavigationWindow _navigationWindow = null!;
        private readonly Setting _setting;
        public HostService(IServiceProvider serviceProvider,Setting setting)
        {
            _setting = setting;
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {

            await HandleActivationAsync();
            ApplicationThemeManager.Apply(_setting.Theme);
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _setting.SaveToFile();
            return Task.CompletedTask;
        }
        private async Task HandleActivationAsync()
        {
            await Task.CompletedTask;

            if (!Application.Current.Windows.OfType<FluentMainWindow>().Any())
            {
                _navigationWindow = (
                    _serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow
                )!;
                _navigationWindow.ShowWindow();
            }

            await Task.CompletedTask;
        }
    }
}
