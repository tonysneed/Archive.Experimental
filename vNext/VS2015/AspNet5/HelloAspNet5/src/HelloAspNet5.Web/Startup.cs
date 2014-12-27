using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using HelloAspNet5.Web.Data;
using HelloAspNet5.Web.Helpers;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.Mvc;

namespace HelloAspNet5.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new Configuration()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework(Configuration)
                .AddSqlServer()
                .AddDbContext<NorthwindSlimContext>();

            services.AddMvc()
                .Configure<MvcOptions>(options =>
                {
                    options.ConfigureJsonFormatters();
                    options.ConfigureXmlFormatters();
                });
            services.AddWebApiConventions();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerfactory)
        {
            loggerfactory.AddConsole();
            app.UseMvc(routes =>
                routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}"));
            app.UseWelcomePage();
        }
    }
}
