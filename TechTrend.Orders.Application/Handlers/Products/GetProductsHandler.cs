using MediatR;
using TechTrend.Orders.Domain.Entities;
using TechTrend.Orders.Infrastructure.Repositories.Products;

namespace TechTrend.Orders.Application.Handlers.Products;

public record GetProductsQuery : IRequest<IEnumerable<Product>>;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productRepository.GetProductsAsync();
    }
}