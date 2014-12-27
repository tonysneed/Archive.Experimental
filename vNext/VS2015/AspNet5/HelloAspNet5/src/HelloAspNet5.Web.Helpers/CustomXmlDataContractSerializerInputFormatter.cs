using System;
using System.Runtime.Serialization;
using Microsoft.AspNet.Mvc;

namespace HelloAspNet5.Web.Helpers
{
    public class CustomXmlDataContractSerializerInputFormatter : XmlDataContractSerializerInputFormatter
    {
        protected override XmlObjectSerializer CreateDataContractSerializer(Type type)
        {
            return DataContractSerializerFactory.CreateDataContractSerializer(type);
        }
    }
}