using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    static public class Helper
    {
        static public List<Product> GenerateProducts(byte numberOfProducts)
        {
            var names = "apple,car,bus,bike,dog,cat,house,phone,knife,gun,shirt,cake".Split(',');
            var random = new Random();
            var products = new List<Product>();
            foreach (var index in Enumerable.Range(1, numberOfProducts))
            {
                var name = names[random.Next(names.Count() - 1)].FirstLetterToUpper();
                products.Add(new Product()
                {
                    Id = Guid.NewGuid(),
                    Code = name.ToLower() + index,
                    Description = $"Description of {name}",
                    Name = name,
                    Price = Math.Round(random.NextDouble() * 100, 2),
                    Quantity = random.Next(100)
                });
            }

            return products;
        }
    }
}
