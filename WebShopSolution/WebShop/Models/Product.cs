namespace WebShop
{
    // Produktmodellen representerar en produkt i webbshoppen
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }

}