using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entities
{
    public class Order
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid ClientId { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        public int Status { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
