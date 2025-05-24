using Domain.Enums;

namespace Domain.DTOs;

public class CreateOrderDTO
{
    public string CustomerEmail { get; set; } = string.Empty;
    public OrderType OrderType { get; set; }
}