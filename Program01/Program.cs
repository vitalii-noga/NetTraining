using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program01
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate 20 products
            var products = GenerateProducts(20);

            // Uses IComparable.CompareTo()
            products.Sort();

            // Print to console
            foreach (var product in products)
                Console.WriteLine(product);

            Console.Read();
        }

        static List<Product> GenerateProducts(byte numberOfProducts)
        {
            var names = "apple,car,bus,bike,dog,cat,house,phone,knife,gun,shirt,cake".Split(',');
            var products = new List<Product>();
            var random = new Random();
            foreach (var index in Enumerable.Range(1, numberOfProducts))
            {
                var name = names[random.Next(names.Count() - 1)].FirstLetterToUpper();
                products.Add(new Product()
                {
                    Id = new Guid(),
                    Code = name.ToLower() + index,
                    Description = "Description of " + name,
                    Name = name,
                    Price = Math.Round(random.NextDouble() * 100, 2)
                });               
            }

            return products;
        }        
    }
}
