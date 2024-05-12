using System.ComponentModel;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrend.Orders.Domain.Entities;

public enum ProductCategory
{
    ApparelAndAccessories = 1,
    Electronics,
    HomeAndGarden,
    HealthAndBeauty,
    SportsAndLeisure,
    ToysAndGames,
    Automotive,
    BooksAndStationary,
    FoodAndBeverage,
    PetSupplies,
    ArtsAndCraft
}

public class Product : Entity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public ProductCategory Category { get; set; }
    public string? ImageUrl { get; set; }

    public void UpdateStock(int quantitySold)
    {
        if (quantitySold > StockQuantity)
            throw new InvalidOperationException("Insufficient stock available.");
        StockQuantity -= quantitySold;
    }
}