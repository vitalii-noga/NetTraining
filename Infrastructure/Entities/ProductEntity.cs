using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common;

namespace Infrastructure.Entities
{
    public class ProductEntity : IComparable
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        [UseForEqualityCheck]
        public string Name { get; set; }
        [UseForEqualityCheck]
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            var inputProduct = obj as ProductEntity;
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
