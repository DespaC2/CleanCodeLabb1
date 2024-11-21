using WebShop.Repositories;

namespace WebShop.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        void NotifyProductAdded(Product product);
    }
}
