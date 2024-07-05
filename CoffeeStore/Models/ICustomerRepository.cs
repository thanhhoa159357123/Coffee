namespace CoffeeStore.Models
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> Customers { get; }

        bool CheckPhoneNumber(string phoneNumber);
        void AddCustomer(Customer newCustomer);
        Customer GetCustomerByPhoneNumber(string phoneNumber);
        void UpdateCustomer(Customer updatedCustomer);
    }
}
