using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities
{
    public class ClientEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
    }
}
