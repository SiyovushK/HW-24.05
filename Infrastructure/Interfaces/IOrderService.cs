using Domain.DTOs;
using Domain.Filters;
using Domain.Responses;

namespace Infrastructure.Interfaces;

public interface IOrderService
{
    Task<Response<GetOrderDTO>> CreateOrderAsync(CreateOrderDTO create);
    Task<Response<GetOrderDTO>> UpdateOrderAsync(int id, UpdateOrderDTO update);
    Task<Response<string>> DeleteOrder(int id);
    Task<Response<GetOrderDTO>> GetByIdAsync(int id);
    Task<PagedResponse<List<GetOrderDTO>>> GetAllAsync(OrderFilter filter);
}