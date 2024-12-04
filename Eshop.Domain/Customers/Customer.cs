using Eshop.Domain.Customers.Events;
using Eshop.Domain.Orders.Rules;
using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Customers;

public class Customer : Entity, IAggregateRoot
{
    public string Name { get; private set; }    

    private Customer(string name) : base(Guid.NewGuid())
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        AddDomainEvent(new CustomerAddedEvent(Id));
    }
    
    public static Customer Create(
        string name)
    {
        CheckRule(new CustomerNameMustHaveOnlyLettersAndCannotBeEmpty(name));

        return new Customer(name);
    }
}