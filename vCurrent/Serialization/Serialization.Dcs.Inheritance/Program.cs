using System;
using System.Xml.Linq;
using Serialization.Shared;

namespace Serialization.Dcs.Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            var person1 = new Person(true) {Name = "John"};
            PrintPerson(person1);

            Console.WriteLine("\nPress Enter to serialize");
            Console.ReadLine();
            var xml = XElement.Parse(Utilities.Serialize(person1, null));
            Console.WriteLine(xml);

            Console.WriteLine("\nPress Enter to deserialize");
            Console.ReadLine();
            var person2 = Utilities.Deserialize<OtherPerson>(xml.ToString(), null);
            PrintPerson(person2);
        }

        private static void PrintPerson(object arg)
        {
            var p = arg as Person;
            if (p != null)
            {
                Console.WriteLine("{0} {1}",
                    p.Name, p.SimpleString);
            }
            var op = arg as OtherPerson;
            if (op != null)
            {
                Console.WriteLine("{0} {1}",
                    op.Name, op.SimpleString);
            }
            var pb = arg as PersonBase;
            if (pb != null)
            {
                foreach (var s in pb.StringCollection)
                {
                    Console.WriteLine("\t{0}", s);
                }
            }
        }
    }
}
