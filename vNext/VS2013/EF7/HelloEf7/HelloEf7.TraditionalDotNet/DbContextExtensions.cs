using System;
using System.Diagnostics;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace HelloEf7.TraditionalDotNet
{
    public static class DbContextExtensions
    {
        public static void LogToConsole(this DbContext context)
        {
            IServiceProvider contextServices = ((IDbContextServices)context).ScopedServiceProvider;
            var loggerFactory = contextServices.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddProvider(new DiagnosticsLoggerProvider
                (new SourceSwitch("SourceSwitch", "Verbose"),
                new ConsoleTraceListener()));
        }
    }
}
