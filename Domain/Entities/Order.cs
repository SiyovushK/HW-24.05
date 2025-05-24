using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public string CustomerEmail { get; set; } = string.Empty;
    public OrderType OrderType { get; set; }
    public double Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeliveryDeadline { get; set; }
    public bool IsDiscountApplied { get; set; }
}