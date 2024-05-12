namespace TechTrend.Orders.Domain.Entities;

public class Customer : Entity
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? DeliveryAddress { get; set; }
}