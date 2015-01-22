namespace Serialization.Dcs
{
    using System;
    using System.Data;
    using System.Runtime.Serialization;
    using System.Xml;

    class Program
    {
        static void Main(string[] args)
        {
            bool serializeInput, configSettings;
            Console.WriteLine("DCS Configuration");

            Console.WriteLine("\nNo settings:");
            NoSettings();

            Console.WriteLine("\nUse Root Name and Namespace:");
            Console.WriteLine("Serialize input? {Y/N}");
            serializeInput = Console.ReadLine().ToUpper() == "Y";
            Console.WriteLine("Configure DCS settings? {Y/N}");
            configSettings = Console.ReadLine().ToUpper() == "Y";
            UseRootNameAndNamespace(serializeInput, configSettings);

            Console.WriteLine("\nUse Known Types:");
            Console.WriteLine("Serialize input? {Y/N}");
            serializeInput = Console.ReadLine().ToUpper() == "Y";
            Console.WriteLine("Configure DCS settings? {Y/N}");
            configSettings = Console.ReadLine().ToUpper() == "Y";
            UseKnownTypes(serializeInput, configSettings);

            Console.WriteLine("\nUse Contract Resolver:");
            Console.WriteLine("Serialize input? {Y/N}");
            serializeInput = Console.ReadLine().ToUpper() == "Y";
            Console.WriteLine("Configure DCS settings? {Y/N}");
            configSettings = Console.ReadLine().ToUpper() == "Y";
            UseDataContractResolver(serializeInput, configSettings);
        }

        private static void NoSettings()
        {
            var settings = new DataContractSerializerSettings();

            //var input = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
            //    "<DummyClass><SampleInt>1</SampleInt></DummyClass>";

            var data = new DummyClass { SampleInt = 1 };
            var input = Utilities.Serialize(data, settings);

            var output = Utilities.Deserialize<DummyClass>(input, settings);
            Console.WriteLine("DummyClass.SampleInt: {0}", output.SampleInt);
        }

        private static void UseRootNameAndNamespace(bool serializeInput, bool configSettings)
        {
            const string SubstituteRootName = "SomeOtherClass";
            const string SubstituteRootNamespace = "http://tempuri.org";

            var settings = new DataContractSerializerSettings();
            if (configSettings)
            {
                var xmlDictionary = new XmlDictionary();
                settings.RootName = xmlDictionary.Add(SubstituteRootName);
                settings.RootNamespace = xmlDictionary.Add(SubstituteRootNamespace);
            }

            string input;
            if (serializeInput)
            {
                var data = new DummyClass { SampleInt = 1 };
                try
                {
                    input = Utilities.Serialize(data, settings);
                }
                catch (SerializationException serializationEx)
                {
                    Console.WriteLine(serializationEx.Message);
                    return;
                }
            }
            else
            {
                input = string.Format(
                    "<{0} xmlns=\"{1}\"><SampleInt xmlns=\"\">1</SampleInt></{0}>",
                    SubstituteRootName,
                    SubstituteRootNamespace);
            }

            try
            {
                var output = Utilities.Deserialize<DummyClass>(input, settings);
                Console.WriteLine("DummyClass.SampleInt: {0}", output.SampleInt);
            }
            catch (SerializationException serializationEx)
            {
                Console.WriteLine(serializationEx.Message);
            }
        }

        private static void UseKnownTypes(bool serializeInput, bool configSettings)
        {
            const string KnownTypeName = "SomeDummyClass";
            const string InstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";
            var settings = new DataContractSerializerSettings();
            if (configSettings)
            {
                settings.KnownTypes = new[] { typeof(SomeDummyClass) };
            }

            string input;
            if (serializeInput)
            {
                var data = new SomeDummyClass { SampleInt = 1, SampleString = "Some text" };
                try
                {
                    input = Utilities.Serialize<DummyClass>(data, settings);
                }
                catch (SerializationException serializationEx)
                {
                    Console.WriteLine(serializationEx.Message);
                    return;
                }
            }
            else
            {
                input = string.Format(
                        "<DummyClass i:type=\"{0}\" xmlns:i=\"{1}\"><SampleInt>1</SampleInt>"
                        + "<SampleString>Some text</SampleString></DummyClass>",
                        KnownTypeName,
                        InstanceNamespace);
            }

            try
            {
                var output = Utilities.Deserialize<DummyClass>(input, settings);
                Console.WriteLine("DummyClass.SampleInt: {0}", output.SampleInt);
            }
            catch (SerializationException serializationEx)
            {
                Console.WriteLine(serializationEx.Message);
            }
        }

        private static void UseDataContractResolver(bool serializeInput, bool configSettings)
        {
            const string KnownTypeName = "SomeDummyClass";
            const string InstanceNamespace = "http://www.w3.org/2001/XMLSchema-instance";
            var settings = new DataContractSerializerSettings();
            if (configSettings)
            {
                settings.DataContractResolver = new DummyClassResolver();
            }

            string input;
            if (serializeInput)
            {
                var data = new SomeDummyClass { SampleInt = 1, SampleString = "Some text" };
                try
                {
                    input = Utilities.Serialize<DummyClass>(data, settings);
                }
                catch (SerializationException serializationEx)
                {
                    Console.WriteLine(serializationEx.Message);
                    return;
                }
            }
            else
            {
                input = string.Format(
                        "<DummyClass i:type=\"{0}\" xmlns:i=\"{1}\"><SampleInt>1</SampleInt>"
                        + "<SampleString>Some text</SampleString></DummyClass>",
                        KnownTypeName,
                        InstanceNamespace);
            }

            try
            {
                var output = Utilities.Deserialize<DummyClass>(input, settings);
                Console.WriteLine("DummyClass.SampleInt: {0}", output.SampleInt);
            }
            catch (SerializationException serializationEx)
            {
                Console.WriteLine(serializationEx.Message);
            }
        }
    }
}
