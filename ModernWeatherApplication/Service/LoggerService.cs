using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ModernWeatherApplication.Service
{
    public class LoggerService
    {
        ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());


        public ILogger CreateLogger(string name) => factory.CreateLogger(name);
    }
}
