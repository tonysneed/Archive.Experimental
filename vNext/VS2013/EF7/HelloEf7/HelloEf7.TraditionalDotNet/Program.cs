using System;
using System.Diagnostics;
using System.Linq;
using HelloEf7.TraditionalDotNet.Contexts;
using HelloEf7.TraditionalDotNet.Models;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.ChangeTracking;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Query;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Fallback;
using Microsoft.Framework.Logging;
using Microsoft.Framework.OptionsModel;

namespace HelloEf7.TraditionalDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new NorthwindSlim())
            {
                // Log to the console
                context.LogToConsole();

                // Get product
                Product product = GetProduct(context);
                PrintProduct(product);
                PrintEntityState(context, product);

                // Update product unit price
                UpdateProductPrice(context, product);
                product = GetProduct(context);
                PrintProduct(product);

                Console.WriteLine("\nPress Enter to exit");
                Console.ReadLine();
            }
        }

        private static void UpdateProductPrice(NorthwindSlim context, Product product)
        {
            // Increment unit price
            Console.WriteLine("\nUpdate all properties {Y}, or just unit price {N}?");
            bool updateAll = Console.ReadLine().ToUpper() == "Y";
            product.UnitPrice++;

            // Attach entity
            context.Attach(product);

            // Mark as modified
            if (updateAll)
                context.Update(product);
            else
                context.Entry(product).Property("UnitPrice").IsModified = true;

            // Save changes
            context.SaveChanges();
        }

        private static Product GetProduct(NorthwindSlim context)
        {
            // Get product
            Console.WriteLine("\nPress Enter to get product");
            Console.ReadLine();
            Product product = context.Products
                .AsNoTracking()
                .Single(p => p.ProductId == 1);
            return product;
        }

        private static void PrintProduct(Product product)
        {
            Console.WriteLine("{0} {1} {2} {3}",
                product.ProductId,
                product.ProductName,
                product.CategoryId,
                product.UnitPrice.GetValueOrDefault().ToString("C"));
        }

        private static void PrintEntityState(NorthwindSlim context, Product product)
        {
            EntityEntry<Product> entry = context.Entry(product);
            Console.WriteLine("Entity State: {0}", entry.State);
        }
    }
}
