using MongoDB.Driver;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.Infrastructure.Repositories.Customers;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoCollection<Customer> _customers;

    public CustomerRepository(IMongoDatabase mongoDatabase)
    {
        _customers = mongoDatabase.GetCollection<Customer>("Customers");
    }
    public async Task<Customer> GetCustomerByIdAsync(Guid customerId)
    {
        return await _customers.Find(c => c.Id == customerId).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Customer>> GetCustomers()
    {
        return await _customers.Find(_ => true).ToListAsync();
    }
}