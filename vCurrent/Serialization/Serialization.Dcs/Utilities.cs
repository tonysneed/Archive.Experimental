namespace Serialization.Dcs
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Text;

    public static class Utilities
    {
        public static string Serialize<TContract>(TContract obj, DataContractSerializerSettings settings)
        {
            var serializer = new DataContractSerializer(typeof(TContract), settings);
            using (var stream = new MemoryStream())
            using (var reader = new StreamReader(stream))
            {
                serializer.WriteObject(stream, obj);
                stream.Position = 0;
                var inputString = reader.ReadToEnd();
                return inputString;
            }
        }

        public static TContract Deserialize<TContract>(string input, DataContractSerializerSettings settings)
        {
            using (Stream stream = new MemoryStream())
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                stream.Write(data, 0, data.Length);
                stream.Position = 0;
                var serializer = new DataContractSerializer(typeof(TContract), settings);
                var output = (TContract)serializer.ReadObject(stream);
                return output;
            }
        }
    }
}
