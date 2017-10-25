using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Infrastructure.Entities;

namespace Infrastructure.Configurations
{
    public class OrderConfiguration : EntityTypeConfiguration<OrderEntity>
    {
        public OrderConfiguration()
        {
            ToTable("dbo.Orders");
            HasKey(x => x.Id);
            HasRequired(x => x.Client).WithMany().HasForeignKey(x => x.ClientId);
            HasMany(x => x.OrderDetails).WithOptional().HasForeignKey(x => x.OrderId);
            Property(x => x.DateCreated).IsRequired();
            Property(x => x.Status).IsOptional();
        }
    }
}
