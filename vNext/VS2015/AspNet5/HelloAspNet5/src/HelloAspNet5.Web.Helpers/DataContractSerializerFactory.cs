using System;
using System.Runtime.Serialization;

namespace HelloAspNet5.Web.Helpers
{
    internal static class DataContractSerializerFactory
    {
        public static DataContractSerializer CreateDataContractSerializer(Type type,
            bool preserveReferences = true)
        {
            var settings = new DataContractSerializerSettings
            {
                PreserveObjectReferences = true
            };
            var serializer = new DataContractSerializer(type, settings);
            return serializer;
        }
    }
}