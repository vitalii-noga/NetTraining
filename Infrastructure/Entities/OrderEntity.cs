using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class OrderEntity
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public DateTime DateCreated { get; set; }
        public int Status { get; set; }

        public virtual ClientEntity Client { get; set; }
        public virtual ICollection<OrderDetailsEntity> OrderDetails { get; set; }
    }
}
