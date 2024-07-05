namespace CoffeeStore.Models
{
    public interface ICoffeeRepository
    {
        IQueryable<Product> Products { get; }

        void SaveProduct(Product p);
        void CreateProduct(Product p);
        void DeleteProduct(Product p);
    }
}