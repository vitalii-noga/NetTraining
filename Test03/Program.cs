using System;
using System.Data.Entity;
using System.Linq;
using Infrastructure;
using Infrastructure.Entities;

namespace Test03
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new NtContext())
            {
                // Remove all records
                db.Products.RemoveRange(db.Products);

                // Generate 20 products and save them
                db.Products.AddRange(Helper.GenerateProducts(20));
                db.SaveChanges();

                // Print all products to console
                Console.WriteLine("Generated products.");
                PrintProducts(db.Products);

                // Update quantity of first 10 products
                db.Products
                    .Take(10)
                    .ToList()
                    .ForEach(x => x.Quantity = 0);
                db.SaveChanges();

                // Print all products again to see the changes
                Console.WriteLine("\nProduct quantities are updated.");
                PrintProducts(db.Products);
            }

            Console.Read();
        }

        static void PrintProducts(DbSet<ProductEntity> products)
        {
            foreach (var product in products)
                Console.WriteLine(product);
        }
    }
}
