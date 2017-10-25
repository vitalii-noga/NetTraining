using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Entities;

namespace Infrastructure.Configurations
{
    public class ProductConfiguration : EntityTypeConfiguration<ProductEntity>
    {
        public ProductConfiguration()
        {
            ToTable("dbo.Products");
            HasKey(x => x.Id);
            Property(x => x.Code).IsRequired();
            Property(x => x.Name).IsRequired();
            Property(x => x.Description).IsOptional();
            Property(x => x.Price).IsRequired();
            Property(x => x.Quantity).IsRequired();            
        }
    }
}
