namespace CoffeeStore.Models
{
    public interface ICoffeeRepository
    {
        IQueryable<Product> Products { get; }
    }
}
