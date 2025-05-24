using Domain.DTOs;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<Response<GetOrderDTO>> CreateOrderAsync(CreateOrderDTO create)
    {
        return await orderService.CreateOrderAsync(create);
    }
    
    [HttpPut]
    public async Task<Response<GetOrderDTO>> UpdateOrderAsync(int id, UpdateOrderDTO update)
    {
        return await orderService.UpdateOrderAsync(id, update);
    }
    
    [HttpDelete]
    public async Task<Response<string>> DeleteOrder(int id)
    {
        return await orderService.DeleteOrder(id);
    }
    
    [HttpGet("{id}")]
    public async Task<Response<GetOrderDTO>> GetByIdAsync(int id)
    {
        return await orderService.GetByIdAsync(id);
    }
    
    [HttpGet]
    public async Task<PagedResponse<List<GetOrderDTO>>> GetAllAsync([FromQuery] OrderFilter filter)
    {
        return await orderService.GetAllAsync(filter);
    }
}