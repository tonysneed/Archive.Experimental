using System;
using System.Net.Http.Formatting;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using WebApiContrib.Formatting;

namespace PocoDemo.Common
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Configure serializer for Json formatter to handle cyclical references.
        /// </summary>
        /// <param name="jsonFormatter">Json media type formatter</param>
        public static void ConfigJsonSerializer(this BaseJsonMediaTypeFormatter jsonFormatter)
        {
            jsonFormatter.SerializerSettings.PreserveReferencesHandling =
                PreserveReferencesHandling.All;
        }

        /// <summary>
        /// Configure serializer for Xml formatter to handle cyclical references.
        /// </summary>
        /// <param name="xmlFormatter">Web API Xml Formatter.</param>
        /// <param name="types">Types returned from controller actions.</param>
        public static void ConfigXmlSerializer
            (this XmlMediaTypeFormatter xmlFormatter, params Type[] types)
        {
            foreach (var type in types)
            {
                var serializer = new DataContractSerializer(type,
                    null, int.MaxValue, false, true, null);
                xmlFormatter.SetSerializer(type, serializer);
            }
        }

        /// <summary>
        /// Configure ProtoBuf formatter to handle cyclical references.
        /// </summary>
        /// <param name="types"></param>
        public static void ConfigProtoBufFormatter(params Type[] types)
        {
            foreach (var type in types)
            {
                var meta = ProtoBufFormatter.Model.Add(type, false);
                var props = type.GetProperties();
                for (var i = 0; i < props.Length; i++)
                {
                    meta.Add(i + 1, props[i].Name);
                }
                meta.AsReferenceDefault = true;
            }
        }
    }
}
