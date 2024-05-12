using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechTrend.Orders.Application.Handlers.Customers;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.API.Controllers.Customers;

[ApiController]
[Route("api/customers")]
public class CustomersController : Controller
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        var query = new GetCustomersQuery();
        var customers = await _mediator.Send(query);
        return Ok(customers);
    }
}