using MongoDB.Bson;
using MongoDB.Driver;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.Infrastructure.Repositories.Orders;

public class OrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders;

    public OrderRepository(IMongoDatabase mongoDatabase)
    {
        _orders = mongoDatabase.GetCollection<Order>("Orders");
    }
    
    
    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        return await _orders.Find(o => o.Id == orderId).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Order>> GetOrdersByCustomerId(Guid customerId)
    {
        return await _orders.Find(o => o.CustomerId == customerId).ToListAsync();
    }

    public async Task CreateOrderAsync(Order order)
    {
        await _orders.InsertOneAsync(order);
    }

    public async Task<Order> UpdateOrderAsync(Guid orderId, UpdateDefinition<Order> orderUpdateDefinition)
    {
        return await _orders.FindOneAndUpdateAsync(o => o.Id == orderId, orderUpdateDefinition);
    }
    
    public Task<IChangeStreamCursor<ChangeStreamDocument<Order>>> GetChangeStream()
    {
        var pipeline = new EmptyPipelineDefinition<ChangeStreamDocument<Order>>()
            .Match(change => change.OperationType == ChangeStreamOperationType.Insert || 
                             change.OperationType == ChangeStreamOperationType.Update);

        var options = new ChangeStreamOptions { FullDocument = ChangeStreamFullDocumentOption.UpdateLookup };

        return _orders.WatchAsync(pipeline, options);
    }
}