using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloEf7.TraditionalDotNet.Contexts;

namespace HelloEf7.TraditionalDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthwindSlim())
            {
                var categories = context.Categories.ToList();
                foreach (var c in categories)
                {
                    Console.WriteLine("{0} {1}",
                        c.CategoryId,
                        c.CategoryName);
                }
            }
        }
    }
}
