namespace CoffeeStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public List<string> Categories { get; set; }
        public List<Product> Products { get; set; }
        public string CurrentCategory { get; set; }
    }
}
