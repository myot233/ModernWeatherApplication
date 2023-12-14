using Microsoft.Extensions.Hosting;
using ModernWeatherApplication.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernWeatherApplication
{
    public class HostService : IHostedService
    {
        private FluentMainWindow _window { get; set; }
        public HostService(FluentMainWindow window) {
            _window = window;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
         
            _window.Show();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
