using WebShop.Notifications;
using WebShop.Repositories;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProductRepository Products { get; private set; }
        private readonly ProductSubject _productSubject;

        public UnitOfWork(IProductRepository productRepository, ProductSubject productSubject = null)
        {
            Products = productRepository;
            _productSubject = productSubject ?? new ProductSubject();

            // Lägg till standard-observatörer
            _productSubject.Attach(new EmailNotification());
        }

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }
    }
}
