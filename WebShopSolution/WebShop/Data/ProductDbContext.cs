using Microsoft.EntityFrameworkCore;
using WebShop.Interfaces;

namespace WebShop.Data
{
    public class ProductDbContext : DbContext, IProductDbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
    }

}
