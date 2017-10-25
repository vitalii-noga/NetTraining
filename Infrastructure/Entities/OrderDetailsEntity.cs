using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class OrderDetailsEntity
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductQuantity { get; set; }

        public virtual OrderEntity Order { get; set; }
        public virtual ProductEntity Product { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; OrderId: {OrderId}; ProductId: {ProductId}; ProductQuantity: {ProductQuantity}.";
        }
    }
}
