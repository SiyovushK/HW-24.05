using Domain.Enums;

namespace Domain.Filters;

public class OrderFilter
{
    public string? CustomerName { get; set; }
    public OrderType? OrderType { get; set; }
    public decimal? Price { get; set; }
    public DateTime? CreatedAt { get; set; }
    public bool? IsDiscountApplied { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}