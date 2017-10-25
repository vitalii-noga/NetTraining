using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Entities;

namespace Infrastructure.Configurations
{
    public class ClientConfiguration : EntityTypeConfiguration<ClientEntity>
    {
        public ClientConfiguration()
        {
            ToTable("dbo.Clients");
            HasKey(x => x.Id);
            Property(x => x.Name).IsRequired();            
        }
    }
}
