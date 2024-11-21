namespace WebShop.Repositories
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll(); 
        void Add(Customer customer); 
    }
}
