﻿using AutoMapper;
using Eshop.Application.Configuration.Commands;
using Eshop.Domain.Customers;
using Eshop.Domain.Orders;
using Eshop.Domain.SeedWork;

namespace Eshop.Application.Orders.CustomerOrder.Commands;

public class CreateCustomerCommandHandler(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateCustomerCommand, Guid>
{
    private readonly ICustomerRepository _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {         
        var customer = Customer.Create(request.Name);

        _customerRepository.Add(customer);

        await _unitOfWork.CommitAsync(cancellationToken);

        return customer.Id;
    }
}