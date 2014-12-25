using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using PocoDemo.Common;
using PocoDemo.Data;
using WebApiContrib.Formatting;

namespace PocoDemo.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Handle cyclical references for xml
            config.Formatters.XmlFormatter.ConfigXmlSerializer
                (typeof(Category), typeof(List<Product>));

            // Handle cyclical references for json
            config.Formatters.JsonFormatter.ConfigJsonSerializer();

            // Handle cyclical references for bson
            var bsonFormatter = new BsonMediaTypeFormatter();
            bsonFormatter.ConfigJsonSerializer();
            config.Formatters.Add(bsonFormatter);

            // Handle cyclical references for protobuf
            SerializationHelper.ConfigProtoBufFormatter
                (typeof(Category), typeof(Product));
            var protoFormatter = new ProtoBufFormatter();
            config.Formatters.Add(protoFormatter);

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
