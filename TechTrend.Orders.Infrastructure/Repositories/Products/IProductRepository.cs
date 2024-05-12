using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.Infrastructure.Repositories.Products;

public interface IProductRepository
{
    Task CreateProductAsync(Product product);
    Task<Product> GetProductByIdAsync(Guid productId);
    Task<IEnumerable<Product>> GetProductsAsync();
}