using MediatR;
using TechTrend.Orders.Domain.Entities;
using TechTrend.Orders.Infrastructure.Repositories.Customers;

namespace TechTrend.Orders.Application.Handlers.Customers;

public record GetCustomersQuery : IRequest<IEnumerable<Customer>>;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, IEnumerable<Customer>>
{

    private readonly ICustomerRepository _customerRepository;

    public GetCustomersHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<IEnumerable<Customer>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return await _customerRepository.GetCustomers();
    }
}