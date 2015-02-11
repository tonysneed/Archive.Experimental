using System;
using MyClient;
using Newtonsoft.Json;

namespace SerializationTest
{
    public class Product : MyEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
