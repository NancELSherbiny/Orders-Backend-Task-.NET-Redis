using AutoMapper;
using Application.DTOs;
using DAL.Models;

namespace API.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Create
            CreateMap<OrderCreateDto, Order>();

            // Update
            CreateMap<OrderUpdateDto, Order>();

            // Response
            CreateMap<Order, OrderResponseDto>();
        }
    }
}
