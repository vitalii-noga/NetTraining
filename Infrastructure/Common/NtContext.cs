using System.Data.Entity;
using Infrastructure.Entities;
using Infrastructure.Configurations;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using CodeFirstStoreFunctions;

namespace Infrastructure
{
    public class NtContext : DbContext
    {
        public DbSet<ClientEntity> Clients { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailsEntity> OrderDetails { get; set; }
        public DbSet<ProductEntity> Products { get; set; }

        public NtContext()
        {
            Database.SetInitializer(new NtContextInitializer());
        }

        public void Cleanup()
        {
            OrderDetails.RemoveRange(OrderDetails);
            Orders.RemoveRange(Orders);
            Clients.RemoveRange(Clients);
            Products.RemoveRange(Products);
            SaveChanges();
        }

        public ObjectResult<ClientDetailsEntity> ClientDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ClientDetailsEntity>("ClientDetails");
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());            
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailsConfiguration());
            modelBuilder.ComplexType<ClientDetailsEntity>();
            modelBuilder.Conventions.Add(new FunctionsConvention<NtContext>("dbo"));
        }
    }
}
