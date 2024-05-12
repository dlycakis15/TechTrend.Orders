using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using TechTrend.Orders.Application.Exceptions;
using TechTrend.Orders.Application.Models;
using TechTrend.Orders.Domain.Entities;
using TechTrend.Orders.Infrastructure.Repositories.Customers;
using TechTrend.Orders.Infrastructure.Repositories.Orders;
using TechTrend.Orders.Infrastructure.Repositories.Products;

namespace TechTrend.Orders.Application.Handlers.Orders;

public record CreateOrderCommand([Required] Guid CustomerId, [Required] List<Guid> Products) : IRequest;


public class CreateOrderHandler : IRequestHandler<CreateOrderCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICustomerRepository _customerRepository;

    public CreateOrderHandler(IOrderRepository orderRepository, IProductRepository productRepository, ICustomerRepository customerRepository)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
    }

    public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var products = new List<Product>();

        var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);

        if (customer == null)
        {
            throw new HttpException($"Unable to find customer with Id {request.CustomerId}", HttpStatusCode.NotFound);
        }
        
        foreach (var productId in request.Products)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);

            if (product == null)
            {
                throw new HttpException($"Product with Id {productId} was not found.", HttpStatusCode.NotFound);
            }

            if (product.StockQuantity <= 0)
            {
                throw new HttpException($"Product with Id {productId} has no stock.",
                    HttpStatusCode.UnprocessableEntity);
            }
            
            products.Add(product);
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            Customer = customer,
            CustomerId = request.CustomerId,
            Products = products,
            OrderDate = DateTimeOffset.Now,
            Status = OrderStatus.Pending
        };

        await _orderRepository.CreateOrderAsync(order);
    }
}