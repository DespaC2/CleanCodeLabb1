using WebShop.Repositories;

namespace WebShop.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        Task SaveChangesAsync(); 
    }
}
