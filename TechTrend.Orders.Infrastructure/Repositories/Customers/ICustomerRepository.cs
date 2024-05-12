using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.Infrastructure.Repositories.Customers;

public interface ICustomerRepository
{
    Task<Customer> GetCustomerByIdAsync(Guid customerId);
    Task<IEnumerable<Customer>> GetCustomers();
}