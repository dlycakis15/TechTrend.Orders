namespace TechTrend.Orders.Infrastructure.Services.Orders;

public interface IOrdersChangedService
{
    Task WatchOrders();
}