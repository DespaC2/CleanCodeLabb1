using WebShop.Data;
using WebShop.Interfaces;
using WebShop.Notifications;
using WebShop.Repositories;

namespace WebShop.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IProductDbContext _context;
        private readonly ProductSubject _productSubject;

        public IProductRepository Products { get; }

        public UnitOfWork(IProductDbContext context, IProductRepository productRepository, ProductSubject productSubject)
        {
            _context = context;
            Products = productRepository;
            _productSubject = productSubject;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void NotifyProductAdded(Product product)
        {
            _productSubject.Notify(product);
        }

        public void Dispose()
        {
            
        }
    }




}
