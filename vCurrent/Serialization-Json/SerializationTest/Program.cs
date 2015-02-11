using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using TrackableEntities;

namespace SerializationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var product1 = new Product
            {
                Id = 1,
                Name = "Product1"
            };

            Console.WriteLine("Press Enter to Xml clone");
            Console.ReadLine();

            product1.TrackingState = TrackingState.Modified;
            product1.ModifiedProperties = new List<string> { "Name" };
            product1.EntityIdentifier = Guid.NewGuid();

            var product2 = XmlClone(product1);
            CompareItems(product1, product2);

            Console.WriteLine("Press Enter to Json clone");
            Console.ReadLine();

            var product3 = JsonClone(product1);
            CompareItems(product1, product3);
        }

        static T XmlClone<T>(T item)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, item);
                stream.Position = 0;
                var result = (T)serializer.ReadObject(stream);
                return result;
            }
        }

        static T JsonClone<T>(T item)
        {
            var json = JsonConvert.SerializeObject(item);
            var result = JsonConvert.DeserializeObject<T>(json);
            return result;
        }

        private static void CompareItems(Product product1, Product product2)
        {
            Debug.Assert(product1.Id == product2.Id);
            Debug.Assert(product1.Name == product2.Name);
            Debug.Assert(product1.TrackingState == product2.TrackingState);
            for (int i = 0; i < product1.ModifiedProperties.Count; i++)
            {
                var prop1 = product1.ModifiedProperties.ElementAt(i);
                var prop2 = product2.ModifiedProperties.ElementAt(i);
                Debug.Assert(prop1 == prop2);
            }
            Debug.Assert(product1.EntityIdentifier == product2.EntityIdentifier);
        }
    }
}
