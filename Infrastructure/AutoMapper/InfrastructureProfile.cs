using AutoMapper;
using Domain.DTOs;
using Domain.Entities;

namespace Infrastructure.AutoMapper;

public class InfrastructureProfile : Profile
{
    public InfrastructureProfile()
    { 
        CreateMap<Order, GetOrderDTO>();
        CreateMap<CreateOrderDTO, Order>();
        CreateMap<UpdateOrderDTO, Order>();
    }
}