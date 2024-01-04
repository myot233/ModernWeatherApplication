using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModernWeatherApplication.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Wpf.Ui;

namespace ModernWeatherApplication.Service
{
    public class HostService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private INavigationWindow _navigationWindow;
        
        public HostService(IServiceProvider serviceProvider)
        {
            
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {

            await HandleActivationAsync();
            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
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

                //_navigationWindow.Navigate(typeof(MainViewPage));
            }

            await Task.CompletedTask;
        }
    }
}
