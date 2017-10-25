using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;
using log4net;
using BusinessLogic.Logic;
using Infrastructure;

namespace Test05
{
    class Program
    {
        static readonly Container container;

        static Program()
        {
            container = new Container();
            container.Register<NtContext>(Lifestyle.Transient);
            container.Register<AggregatedCalculator>(Lifestyle.Transient);
            container.RegisterSingleton<ILog>(LogManager.GetLogger("Task05"));
        }

        static void Main(string[] args)
        {
            PopulateData();

            var db = container.GetInstance<NtContext>();
            var calc = container.GetInstance<AggregatedCalculator>();
            var orderId = db.Orders.First().Id;
            var orderDetails = db.OrderDetails.Where(x => x.OrderId == orderId);

            // Print Order details and Order total
            Console.WriteLine("Test an AggregatedCalculator.GetOrderTotal function.\n");
            Console.WriteLine($"Order Id: {orderId}");
            foreach (var detail in orderDetails)
            {
                var product = db.Products.Find(detail.ProductId);
                Console.WriteLine($"Product: {product.Name}; Price: {product.Price}; Quantity: {detail.ProductQuantity}; Total: {product.Price * detail.ProductQuantity}");
            }

            Console.WriteLine($"\nOrder Total: {calc.GetOrderTotal(orderId)}.");

            // Print the last 10 Orders use an Include method
            Console.WriteLine("\n\nTest an AggregatedCalculator.GetLastOrders function with an Include method.\n");
            var sw = new Stopwatch();
            sw.Start();
            PrintTheLastOrders(10, true);
            Console.WriteLine($"Elapsed time is {sw.ElapsedMilliseconds} milliseconds.");

            // Print the last 10 Orders do not use an Include method
            Console.WriteLine("\n\nTest an AggregatedCalculator.GetLastOrders function without an Include method.\n");
            sw.Restart();
            PrintTheLastOrders(10, false);
            Console.WriteLine($"Elapsed time is {sw.ElapsedMilliseconds} milliseconds.");

            // Print client details
            var clients = db.ClientDetails().ToList();
            Console.WriteLine("\n\nTest ClientDetails db procedure");
            Console.WriteLine($"Number of clients: {clients.Count()}");
            foreach (var client in clients)
            {
                Console.WriteLine(client);
            }

            Console.ReadKey();
        }

        static void PopulateData()
        {
            var db = container.GetInstance<NtContext>();
            db.Cleanup();

            // Add 10 clients, 50 products, 20 orders and 5 order details for each order
            db.Clients.AddRange(Helper.GenerateClients(10));
            db.Products.AddRange(Helper.GenerateProducts(50));
            db.SaveChanges();
            db.Orders.AddRange(Helper.GenerateOrders(db.Clients.ToList(), 20));
            db.SaveChanges();
            db.OrderDetails.AddRange(Helper.GenerateOrderDetails(db.Orders.ToList(), db.Products.ToList(), 5));
            db.SaveChanges();
        }

        static void PrintTheLastOrders(int numberOfOrders, bool useIncludeMethod)
        {
            var calc = container.GetInstance<AggregatedCalculator>();
            var orders = calc.GetLastOrders(numberOfOrders, useIncludeMethod);
            Console.WriteLine($"The last {orders.Count} orders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"Id: {order.Id}; Total: {order.Total}");
            }

            if (orders.Count > 0)
            {
                var orderId = orders.First().Id;
                Console.WriteLine($"\nCheck the total of the last order use an AggregatedCalculator.GetOrderTotal function:");
                Console.WriteLine($"Id: {orderId}; Total: {calc.GetOrderTotal(orderId)}");
            }
        }
    }
}
