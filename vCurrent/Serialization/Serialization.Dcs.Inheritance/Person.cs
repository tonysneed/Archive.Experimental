using System.Collections.Generic;

namespace Serialization.Dcs.Inheritance
{
    public class Person : PersonBase
    {
        public Person() { }

        public Person(bool init)
        {
            SimpleString = "Simple String";
            StringCollection = new List<string>
            {
                "Item1", "Item2", "Item3"
            };
        }

        public string Name { get; set; }
    }
}
