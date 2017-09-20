using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace Task02
{
    class Program
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger("Task02");

        static void Main(string[] args)
        {
            // Generate 20 products and print it
            var products = Helper.GenerateProducts(20);
            Console.WriteLine("List of all products:");
            PrintProducts(products);

            // Get unique products and print it
            var dict = new Dictionary<Product, string>(new ProductEqualityComparer());
            foreach (var product in products)
            {
                if (!dict.ContainsKey(product))
                    dict.Add(product, product.Name);
            }

            Console.WriteLine("\nDistinct list of products:");
            PrintProducts(dict.Keys.ToList());

            Console.Read();
        }

        static void PrintProducts(List<Product> products)
        {
            log.Info($"Number of products to print is {products.Count()}.");
            foreach (var product in products)
            {
                var productInfo = product.ToString();
                Console.WriteLine(productInfo);
                log.Info(productInfo);
            }
        }
    }
}
