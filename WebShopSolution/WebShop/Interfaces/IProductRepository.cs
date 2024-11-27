namespace WebShop.Repositories
{
    // Gränssnitt för produktrepositoryt enligt Repository Pattern
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task DeleteAsync(int id);
        Task UpdateAsync(Product product);
    }
}
