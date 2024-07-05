namespace CoffeeStore.Models
{
    public class EFCoffeeRepository : ICoffeeRepository
    {
        private readonly CoffeeDbContext context;
        public EFCoffeeRepository(CoffeeDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;

        public void CreateProduct(Product p)
        {
            context.Add(p);
            context.SaveChanges();
        }
        public void DeleteProduct(Product p)
        {
            context.Remove(p);
            context.SaveChanges();
        }
        public void SaveProduct(Product p)
        {
            context.SaveChanges();
        }
    }
}