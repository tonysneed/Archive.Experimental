using System;
using System.Web.Http;
using Newtonsoft.Json;

namespace NTierEf7.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Handle cyclical references for json
            config.Formatters.JsonFormatter.SerializerSettings.
                PreserveReferencesHandling = PreserveReferencesHandling.All;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
