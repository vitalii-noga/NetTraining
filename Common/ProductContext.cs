using System.Data.Entity;

namespace Common
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
