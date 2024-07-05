using Microsoft.EntityFrameworkCore;

namespace CoffeeStore.Models
{
    public class EFCustomerRepository : ICustomerRepository
    {
        private readonly CoffeeDbContext context;
        public EFCustomerRepository(CoffeeDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Customer> Customers => context.Customers;
        public void UpdateCustomer(Customer updatedCustomer)
        {
            // Cập nhật thông tin người dùng trong cơ sở dữ liệu
            context.Customers.Update(updatedCustomer);
            context.SaveChanges();
        }
        public void AddCustomer(Customer newCustomer)
        {
            context.Customers.Add(newCustomer);
            context.SaveChanges();
        }

        public Customer GetCustomerByPhoneNumber(string phoneNumber) => context.Customers.FirstOrDefault(c => c.Phone == phoneNumber);
        public bool CheckPhoneNumber(string phoneNumber)
        {
            return context.Customers.Any(c => c.Phone == phoneNumber);
        }
    }
}
