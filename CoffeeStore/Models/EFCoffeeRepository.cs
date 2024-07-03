namespace CoffeeStore.Models
{
    public class EFCoffeeRepository : ICoffeeRepository
    {
        private CoffeeDbContext context;
        public EFCoffeeRepository(CoffeeDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Product> Products => context.Products;

    }
}
