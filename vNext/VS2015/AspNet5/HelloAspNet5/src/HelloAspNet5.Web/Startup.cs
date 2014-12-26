using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace HelloAspNet5.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.Configure<ILoggerFactory>(options =>
            //{
            //    options.AddProvider();
            //});
            //services.ConfigureOptions<ILoggerFactory>().AddTransient<>()
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseWelcomePage();
        }
    }
}
