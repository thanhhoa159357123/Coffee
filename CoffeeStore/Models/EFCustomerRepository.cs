namespace CoffeeStore.Models
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private CoffeeDbContext context;
        public EFCustomerRepository(CoffeeDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Customer> Customers => context.Customers;
        public bool CheckPhoneNumber(string phoneNumber)
        {
            return context.Customers.Any(c => c.Phone == phoneNumber);
        }
        public void UpdateCustomer(Customer updatedCustomer)
        {
            // Cập nhật thông tin người dùng trong cơ sở dữ liệu
            context.Customers.Update(updatedCustomer);
            context.SaveChanges();
        }
    }
}
