using MediatR;
using TechTrend.Orders.Domain.Entities;
using TechTrend.Orders.Infrastructure.Repositories.Orders;

namespace TechTrend.Orders.Application.Handlers.Orders;

public record GetOrdersByCustomerQuery : IRequest<IEnumerable<Order>>
{
    public Guid CustomerId;
}

public class GetOrdersByCustomerHandler : IRequestHandler<GetOrdersByCustomerQuery, IEnumerable<Order>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersByCustomerHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<Order>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetOrdersByCustomerId(request.CustomerId);
    }
}