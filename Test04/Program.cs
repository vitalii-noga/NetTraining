using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using Infrastructure;

namespace Test04
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NtContext())
            {
                db.Cleanup();

                // Add 10 clients and 50 products
                db.Clients.AddRange(Helper.GenerateClients(10));
                db.Products.AddRange(Helper.GenerateProducts(50));

                // Apply changes
                db.SaveChanges();

                // Test the performance of adding orders 3 times
                foreach (var index in Enumerable.Range(1, 3))
                {                    
                    // Remove orders
                    db.Orders.RemoveRange(db.Orders);
                    db.SaveChanges();
                    // Add orders
                    Console.WriteLine($"\n{index} Iterration:");
                    MeasurePerformanceOfAddingOrders(db);
                }                

                // Add 10 OrderDetails with using SqlBulkCopy for each order
                using (var connection = new SqlConnection(db.Database.Connection.ConnectionString))
                {
                    connection.Open();
                    using (var bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "dbo.OrderDetails";
                        try
                        {
                            var sw = new Stopwatch();
                            sw.Start();
                            bulkCopy.WriteToServer(Helper.GenerateOrderDetailsTable(db.Orders.ToList(), db.Products.ToList(), 10));
                            sw.Stop();
                            Console.WriteLine($"\nThe population {db.Orders.Count()} orders with using SqlBulkCopy took {sw.ElapsedMilliseconds} milliseconds.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                Console.WriteLine($"\nPlease press any key to display {db.OrderDetails.Count()} order details.");
                Console.ReadKey();

                foreach (var details in db.OrderDetails)
                    Console.WriteLine(details);
            }
            Console.ReadKey();
        }

        static void MeasurePerformanceOfAddingOrders(NtContext db)
        {
            // Add 500 orders with notmal context
            var sw = new Stopwatch();
            sw.Start();
            db.Orders.AddRange(Helper.GenerateOrders(db.Clients.ToList(), 500));
            db.SaveChanges();
            sw.Stop();
            int firstStageOrderCount = db.Orders.Count();
            long firstStageElapsedTime = sw.ElapsedMilliseconds;
            Console.WriteLine($"The adding {firstStageOrderCount} orders to database with using normal context took {firstStageElapsedTime} milliseconds.");

            // Add 500 orders where automatic detection of changes disabled
            db.Configuration.AutoDetectChangesEnabled = false;
            try
            {
                sw.Reset();
                sw.Start();
                db.Orders.AddRange(Helper.GenerateOrders(db.Clients.ToList(), 500));
                db.SaveChanges();
                sw.Stop();
                Console.WriteLine($"The adding {db.Orders.Count() - firstStageOrderCount} orders to database where automatic detection of changes disabled " +
                    $"took {sw.ElapsedMilliseconds} milliseconds.");                
            }
            finally
            {
                db.Configuration.AutoDetectChangesEnabled = true;
            }

            // Print the performance measure
            if (sw.ElapsedMilliseconds < firstStageElapsedTime)
            {
                Console.WriteLine($"The second stage has BETTER performance in " +
                    $"{Math.Round((double)firstStageElapsedTime / sw.ElapsedMilliseconds, 2)} times than the first stage.");
            }
            if (sw.ElapsedMilliseconds > firstStageElapsedTime)
            {
                Console.WriteLine($"The second stage has WORSE performance in " +
                    $"{Math.Round((double)sw.ElapsedMilliseconds / firstStageElapsedTime, 2)} times than the first stage.");
            }
            else
            {
                Console.WriteLine($"The second stage has the same performance as the first one.");
            }
        }
    }
}
