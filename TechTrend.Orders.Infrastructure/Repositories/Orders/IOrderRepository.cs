using MongoDB.Driver;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.Infrastructure.Repositories.Orders;

public interface IOrderRepository
{
    Task<Order> GetOrderByIdAsync(Guid orderId);
    Task<IEnumerable<Order>> GetOrdersByCustomerId(Guid customerId);
    Task CreateOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Guid orderId, UpdateDefinition<Order> orderUpdateDefinition);
    Task<IChangeStreamCursor<ChangeStreamDocument<Order>>> GetChangeStream();
}