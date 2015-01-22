using System;
namespace Serialization.Dcs
{
    using System.Runtime.Serialization;
    using System.Xml;

    public class DummyClassResolver : DataContractResolver
    {
        public override bool TryResolveType(
            Type type,
            Type declaredType,
            DataContractResolver knownTypeResolver,
            out XmlDictionaryString typeName,
            out XmlDictionaryString typeNamespace)
        {
            if (declaredType == typeof(DummyClass))
            {
                XmlDictionary dictionary = new XmlDictionary();
                typeName = dictionary.Add("SomeDummyClass");
                typeNamespace = dictionary.Add(string.Empty);
                return true;
            }
            return knownTypeResolver.TryResolveType(declaredType, declaredType, null, out typeName, out typeNamespace);
        }

        public override Type ResolveName(string typeName, string typeNamespace, Type declaredType, DataContractResolver knownTypeResolver)
        {
            if (typeName == "SomeDummyClass" && typeNamespace == string.Empty)
            {
                return typeof(DummyClass);
            }

            return knownTypeResolver.ResolveName(typeName, typeNamespace, null, knownTypeResolver);
        }
    }
}
