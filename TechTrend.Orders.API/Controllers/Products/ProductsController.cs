using MediatR;
using Microsoft.AspNetCore.Mvc;
using TechTrend.Orders.Application.Handlers.Products;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.API.Controllers.Products;

[ApiController]
[Route("api/products")]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var query = new GetProductsQuery();
        var products = await _mediator.Send(query);
        return Ok(products);
    }
}