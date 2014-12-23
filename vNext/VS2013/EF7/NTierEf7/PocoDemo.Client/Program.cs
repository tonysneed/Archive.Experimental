using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PocoDemo.Common;
using PocoDemo.Data;
using WebApiContrib.Formatting;

namespace PocoDemo.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press Enter to start");
            Console.ReadLine();

            // Configure accept header and media type formatter
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();
            ((BaseJsonMediaTypeFormatter)formatter).ConfigJsonSerializer();

            // Create an http client with service base address
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:51247/api/"),
            };

            // Set request accept header
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Get response
            HttpResponseMessage response = client.GetAsync("customers").Result;
            response.EnsureSuccessStatusCode();

            // Read response content
            var customers = response.Content.ReadAsAsync<List<Customer>>
                (new[] { formatter }).Result;
            foreach (var c in customers)
            {
                Console.WriteLine("{0} {1} {2}",
                    c.CustomerId,
                    c.CompanyName,
                    c.City);
            }

            // Create order for a customer
            Console.WriteLine("\nCreate order for {CustomerId}:");
            string customerId = Console.ReadLine();

            var newOrder = new Order
            {
                CustomerId = customerId,
                OrderDate = DateTime.Today,
                ShippedDate = DateTime.Today.AddDays(1),
                OrderDetails = new List<OrderDetail>
                    {
                        new OrderDetail { ProductId = 1, Quantity = 5, UnitPrice = 10 },
                        new OrderDetail { ProductId = 2, Quantity = 10, UnitPrice = 20 },
                        new OrderDetail { ProductId = 4, Quantity = 40, UnitPrice = 40 }
                    }
            };
            var order = CreateOrder(client, formatter, newOrder);
            PrintOrderWithDetails(order);

            // Update the order
            Console.WriteLine("\nPress Enter to update order details");
            Console.ReadLine();
            order.OrderDate = order.OrderDate.GetValueOrDefault().AddDays(1);
            order = UpdateOrder(client, formatter, order);
            PrintOrderWithDetails(order);

            // Delete the order
            Console.WriteLine("\nPress Enter to delete the order");
            Console.ReadLine();
            DeleteOrder(client, order);

            // Verify order was deleted
            var deleted = VerifyOrderDeleted(client, order.OrderId);
            Console.WriteLine(deleted ?
                "Order was successfully deleted" :
                "Order was not deleted");

            // Keep console open
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }

        private static Order CreateOrder(HttpClient client, MediaTypeFormatter formatter, Order order)
        {
            string request = "orders";
            var response = client.PostAsync<Order>(request, order, formatter).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<Order>(new[] { formatter }).Result;
            return result;
        }

        private static Order UpdateOrder(HttpClient client, MediaTypeFormatter formatter, Order order)
        {
            string request = "Orders";
            var response = client.PutAsync<Order>(request, order, formatter).Result;
            response.EnsureSuccessStatusCode();
            var result = response.Content.ReadAsAsync<Order>(new[] { formatter }).Result;
            return result;
        }

        private static void DeleteOrder(HttpClient client, Order order)
        {
            string request = "Orders/" + order.OrderId;
            var response = client.DeleteAsync(request).Result;
            response.EnsureSuccessStatusCode();
        }

        private static bool VerifyOrderDeleted(HttpClient client, int orderId)
        {
            string request = "Orders/" + orderId;
            var response = client.GetAsync(request).Result;
            if (response.IsSuccessStatusCode) return false;
            return true;
        }

        private static void PrintOrderWithDetails(Order o)
        {
            Console.WriteLine("{0} {1}",
                o.OrderId,
                o.OrderDate.GetValueOrDefault().ToShortDateString());
            foreach (var od in o.OrderDetails)
            {
                Console.WriteLine("\t{0} {1} {2} {3}",
                    od.OrderDetailId,
                    od.ProductId,
                    od.Quantity,
                    od.UnitPrice.ToString("c"));
            }
        }
    }
}
