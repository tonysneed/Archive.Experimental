using System;
using System.Runtime.Serialization;
using Microsoft.AspNet.Mvc;

namespace HelloAspNet5.Web.Helpers
{
    public class CustomXmlDataContractSerializerOutputFormatter : XmlDataContractSerializerOutputFormatter
    {
        protected override DataContractSerializer CreateSerializer(Type type)
        {
            return DataContractSerializerFactory.CreateDataContractSerializer(type);
        }
    }
}