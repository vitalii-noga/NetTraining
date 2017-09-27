using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common.Entities
{
    public class Client
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
