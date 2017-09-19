using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Program01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate 20 products
            var products = Helper.GenerateProducts(20);

            // Uses IComparable.CompareTo()
            products.Sort();

            // Print to console
            foreach (var product in products)
                Console.WriteLine(product);

            Console.Read();
        }
    }
}
