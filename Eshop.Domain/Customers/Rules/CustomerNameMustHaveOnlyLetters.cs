using System.Text.RegularExpressions;
using Eshop.Domain.SeedWork;

namespace Eshop.Domain.Orders.Rules;

public class CustomerNameMustHaveOnlyLettersAndCannotBeEmpty(string customerName) : IBusinessRule
{
    public bool IsBroken() => !Regex.IsMatch(customerName, "^[a-zA-Z]+$");

    public string Message => "Customer name must have only letters and cannot be empty.";
}