using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrend.Orders.Domain.Entities;

public class Entity
{
    public Guid Id { get; set; }
}