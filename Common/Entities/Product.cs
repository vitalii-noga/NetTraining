using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Product : IComparable
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [UseForEqualityCheck]
        public string Name { get; set; }
        [UseForEqualityCheck]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        public ICollection<OrderDetails> OrderDetails { get; set; }

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
            return $"Code: {Code}; Name: {Name}; Price: {Price}; Quantity: {Quantity}";
        }
    }
}
