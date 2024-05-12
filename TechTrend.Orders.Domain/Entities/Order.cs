using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrend.Orders.Domain.Entities;

public enum OrderStatus
{
    Pending = 1,
    Completed,
    Cancelled
}

public class Order : Entity
{
    [BsonElement("customer_id")]
    public Guid CustomerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    [BsonRepresentation(BsonType.String)]
    public OrderStatus Status { get; set; }
    public DateTimeOffset? DispatchDate { get; set; }
    public DateTimeOffset? CancelledAtDate { get; set; }
    public Customer Customer { get; set; }
    public List<Product> Products { get; set; }
}