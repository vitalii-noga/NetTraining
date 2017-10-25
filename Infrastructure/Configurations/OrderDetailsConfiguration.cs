using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Entities;

namespace Infrastructure.Configurations
{
    public class OrderDetailsConfiguration : EntityTypeConfiguration<OrderDetailsEntity>
    {
        public OrderDetailsConfiguration()
        {
            ToTable("dbo.OrderDetails");
            HasKey(x => x.Id);
            HasRequired(x => x.Order).WithMany().HasForeignKey(x => x.OrderId);
            HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            Property(x => x.ProductQuantity).IsRequired();
    }
    }
}
