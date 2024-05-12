using Azure.Messaging.ServiceBus;
using MongoDB.Bson;
using MongoDB.Driver;
using TechTrend.Orders.Domain.Entities;
using TechTrend.Orders.Infrastructure.Repositories.Orders;

namespace TechTrend.Orders.Infrastructure.Services.Orders;

public class OrdersChangedService : IOrdersChangedService
{
    private readonly IOrderRepository _orderRepository;
    //private readonly ServiceBusSender _serviceBusSender;

    public OrdersChangedService(IOrderRepository orderRepository /*ServiceBusClient serviceBusClient*/)
    {
        _orderRepository = orderRepository;
        //_serviceBusSender = serviceBusClient.CreateSender("order-changed");

    }

    public async Task WatchOrders()
    {
        var changeStream = await _orderRepository.GetChangeStream();
        foreach (var change in changeStream.ToEnumerable())
        {
            HandleChange(change).GetAwaiter().GetResult();
        }
    }

    private async Task HandleChange(ChangeStreamDocument<Order> change)
    {
        Console.WriteLine($"Detected change for order {change.FullDocument.Id}");
        /*var message = new ServiceBusMessage(change.FullDocument.ToJson())
        {
            MessageId = change.FullDocument.Id.ToString(),
            Subject = $"Notification {change.OperationType.ToString()} Event",
            CorrelationId = Guid.NewGuid().ToString()
        };

        await _serviceBusSender.SendMessageAsync(message); */
    }
}