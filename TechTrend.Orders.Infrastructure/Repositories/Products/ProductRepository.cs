using MongoDB.Driver;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.Infrastructure.Repositories.Products;

public class ProductRepository : IProductRepository
{
    private readonly IMongoCollection<Product> _products;

    public ProductRepository(IMongoDatabase mongoDatabase)
    {
        _products = mongoDatabase.GetCollection<Product>("Products");
    }
    
    public async Task CreateProductAsync(Product product)
    {
        await _products.InsertOneAsync(product);
    }

    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        return await _products.Find(p => p.Id == productId).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _products.Find(_ => true).ToListAsync();
    }
}