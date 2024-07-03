namespace CoffeeStore.Models
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> Customers { get; }

        bool CheckPhoneNumber(string phoneNumber);
        void UpdateCustomer(Customer updatedCustomer);
    }
}
