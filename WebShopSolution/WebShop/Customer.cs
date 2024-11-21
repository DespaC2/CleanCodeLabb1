using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebShop
{
    public class Customer
    {
        public int Id { get; set; } // Unikt ID för produkten
        public string Name { get; set; } // Namn på produkten

        [EmailAddress] 
        public string EmailAddress { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }
    }
}
