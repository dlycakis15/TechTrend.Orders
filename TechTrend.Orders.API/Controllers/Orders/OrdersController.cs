using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechTrend.Orders.Application.Handlers.Orders;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.API.Controllers.Orders;

[ApiController]
[Route("api/orders")]
public class OrdersController : Controller
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        await _mediator.Send(command);
        return Accepted();
    }

    [HttpGet("{customerId:guid}")]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrderByCustomer(Guid customerId)
    {
        var query = new GetOrdersByCustomerQuery
        {
            CustomerId = customerId
        };

        var orders = await _mediator.Send(query);

        return Ok(orders);
    }
}