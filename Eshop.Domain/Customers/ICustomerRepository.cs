using Eshop.Domain.Customers;

namespace Eshop.Domain.Orders;

public interface ICustomerRepository
{
    Task<Customer> GetByIdAsync(Guid id);

    void Add(Customer customer);
}