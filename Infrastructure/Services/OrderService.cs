using System.Net;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using Domain.Filters;
using Domain.Responses;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class OrderService(IBaseRepository<Order, int> orderRepository, IMapper mapper) : IOrderService
{
    public async Task<Response<GetOrderDTO>> CreateOrderAsync(CreateOrderDTO create)
    {

        var order = new Order()
        {
            CustomerEmail = create.CustomerEmail,
            OrderType = create.OrderType,
            Price = 100,
            CreatedAt = DateTime.UtcNow,
            IsDiscountApplied = false
        };

        if (create.OrderType == Domain.Enums.OrderType.Standart)
            order.DeliveryDeadline = DateTime.UtcNow.AddHours(24);

        if (create.OrderType == Domain.Enums.OrderType.Economy)
            order.DeliveryDeadline = DateTime.UtcNow.AddHours(72);

        if (create.OrderType == Domain.Enums.OrderType.Express)
            order.DeliveryDeadline = DateTime.UtcNow.AddHours(2);

        var result = await orderRepository.AddAsync(order);

        if (result == 0)
        {
            return new Response<GetOrderDTO>(HttpStatusCode.BadRequest, "Order not created");
        }

        var dto = mapper.Map<GetOrderDTO>(order);
        return new Response<GetOrderDTO>(dto);
    }

    public async Task<Response<GetOrderDTO>> UpdateOrderAsync(int id, UpdateOrderDTO update)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return new Response<GetOrderDTO>(HttpStatusCode.NotFound, "Order not found");
        }

        if (update.OrderType == Domain.Enums.OrderType.Standart)
            order.DeliveryDeadline = DateTime.UtcNow.AddHours(24);

        if (update.OrderType == Domain.Enums.OrderType.Economy)
            order.DeliveryDeadline = DateTime.UtcNow.AddHours(72);

        if (update.OrderType == Domain.Enums.OrderType.Express)
            order.DeliveryDeadline = DateTime.UtcNow.AddHours(2);

        mapper.Map(update, order);

        var result = await orderRepository.UpdateAsync(order);
        if (result == 0)
        {
            return new Response<GetOrderDTO>(HttpStatusCode.BadRequest, "Order not updated");
        }

        var dto = mapper.Map<GetOrderDTO>(order);
        return new Response<GetOrderDTO>(dto);
    }

    public async Task<Response<string>> DeleteOrder(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return new Response<string>(HttpStatusCode.NotFound, "Order not found");
        }

        var result = await orderRepository.DeleteAsync(order);
        if (result == 0)
        {
            return new Response<string>(HttpStatusCode.BadRequest, "Order not deleted");
        }

        return new Response<string>("Order deleted successfully");
    }

    public async Task<PagedResponse<List<GetOrderDTO>>> GetAllAsync(OrderFilter filter)
    {
        var pageNumber = filter.PageNumber <= 0 ? 1 : filter.PageNumber;
        var pageSize = filter.PageSize < 10 ? 10 : filter.PageSize;

        var query = await orderRepository.GetAllAsync();
        var totalRecords = await query.CountAsync();

        var pagedOrders = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var dtos = mapper.Map<List<GetOrderDTO>>(pagedOrders);

        return new PagedResponse<List<GetOrderDTO>>(dtos, pageNumber, pageSize, totalRecords);
    }

    public async Task<Response<GetOrderDTO>> GetByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order == null)
        {
            return new Response<GetOrderDTO>(HttpStatusCode.NotFound, "Order not found");
        }

        var dto = mapper.Map<GetOrderDTO>(order);
        return new Response<GetOrderDTO>(dto);
    }
}