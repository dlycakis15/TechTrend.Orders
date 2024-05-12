using MongoDB.Driver;
using TechTrend.Orders.Domain.Entities;

namespace TechTrend.Orders.Domain.Seed;


public class MongoDbInitialiser
{
    private readonly IMongoDatabase _database;

    public MongoDbInitialiser(IMongoDatabase mongoDatabase)
    {
        _database = mongoDatabase;
    }

    public void Initialize()
    {
        CreateCollections();
        SeedData();
    }

    private void CreateCollections()
    {
        var collectionNames = _database.ListCollectionNames().ToList();
        if (!collectionNames.Contains("Customers"))
        {
            _database.CreateCollection("Customers");
        }
        if (!collectionNames.Contains("Products"))
        {
            _database.CreateCollection("Products");
        }
        if (!collectionNames.Contains("Orders"))
        {
            _database.CreateCollection("Orders");
        }
        // Add more collections as necessary
    }

    private void SeedData()
    {
        var userCollection = _database.GetCollection<Customer>("Customers");
        if (userCollection.CountDocuments(FilterDefinition<Customer>.Empty) == 0)
        {
            userCollection.InsertOne(new Customer
            {
                Firstname = "Chris", Lastname = "Pine", Email = "chris@example.com", PhoneNumber = "0425845202",
                DeliveryAddress = "93 fake street, Cronulla"
            });
            userCollection.InsertOne(new Customer
            {
                Firstname = "David", Lastname = "Joel", Email = "david@example.com", PhoneNumber = "0425845202",
                DeliveryAddress = "90 fake street, Cronulla"
            });
        }

        var productCollection = _database.GetCollection<Product>("Products");
        if (productCollection.CountDocuments(FilterDefinition<Product>.Empty) == 0)
        {
            productCollection.InsertMany(new []
            {
                new Product { Id = Guid.NewGuid(), Name = "Eco-Friendly Running Shoes", Description = "Lightweight, breathable, and designed for speed", Price = 120.00m, StockQuantity = 150, Category = ProductCategory.ApparelAndAccessories, ImageUrl = "http://example.com/products/shoes.jpg" },
                new Product { Id = Guid.NewGuid(), Name = "Bluetooth Headphones", Description = "Wireless headphones with noise-cancelling features", Price = 200.00m, StockQuantity = 100, Category = ProductCategory.Electronics, ImageUrl = "http://example.com/products/headphones.jpg" },
                new Product { Id = Guid.NewGuid(), Name = "Organic Green Tea", Description = "A soothing blend of 100% organic green tea leaves", Price = 15.00m, StockQuantity = 200, Category = ProductCategory.FoodAndBeverage, ImageUrl = "http://example.com/products/tea.jpg" },
                new Product { Id = Guid.NewGuid(), Name = "Mountain Bike", Description = "Rugged mountain bike designed for off-road trails", Price = 800.00m, StockQuantity = 50, Category = ProductCategory.SportsAndLeisure, ImageUrl = "http://example.com/products/bike.jpg" },
                new Product { Id = Guid.NewGuid(), Name = "Professional Watercolor Set", Description = "24-color set perfect for artists of all skill levels", Price = 35.00m, StockQuantity = 80, Category = ProductCategory.ArtsAndCraft, ImageUrl = "http://example.com/products/paint.jpg" }
            });
        }
    }
}