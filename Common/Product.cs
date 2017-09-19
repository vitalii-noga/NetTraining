using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Product : IComparable
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var inputProduct = obj as Product;
            if (inputProduct == null)
                throw new ArgumentException("Input object is not a Product", "obj");
            else
                return Code.CompareTo(inputProduct.Code);
        }

        public override string ToString()
        {
            return $"Code: {Code}; Name: {Name}; Price: {Price}";
        }
    }
}
