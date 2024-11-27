using Microsoft.EntityFrameworkCore;

namespace WebShop.Interfaces
{
    public interface IProductDbContext
    {
        DbSet<Product> Products { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
