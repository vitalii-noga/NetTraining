using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    public class ClientDetailsEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double OrdersTotal { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}; Name: {Name}; Orders Total: {OrdersTotal}";
        }
    }
}
