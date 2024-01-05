using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ModernWeatherApplication.Service
{
    public static class LoggerService
    {
        static readonly ILoggerFactory Factory = LoggerFactory.Create(builder => builder.AddConsole());


        public static ILogger CreateLogger(string name) => Factory.CreateLogger(name);

        public static ILogger CreateLogger(Type clsName) => CreateLogger(clsName.Name);


    }
}
