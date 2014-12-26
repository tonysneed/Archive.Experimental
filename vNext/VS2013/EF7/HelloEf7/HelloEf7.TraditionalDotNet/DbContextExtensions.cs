using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;

namespace HelloEf7.TraditionalDotNet
{
    public static class DbContextExtensions
    {
        public static void LogToConsole(this DbContext context)
        {
            IServiceProvider contextServices = ((IDbContextServices)context).ScopedServiceProvider;
            var loggerFactory = contextServices.GetRequiredService<ILoggerFactory>();
            loggerFactory.AddConsole(LogLevel.Verbose);
        }
    }
}
