using AutoMapper;
using OrderManagementSystem.Data.Entities;
using OrderManagementSystem.Data.Entities.Basket;
using OrderManagementSystem.Services.Dtos;
using Talabat.Core.Entities.Basket;
using Talabate.PL.Dtos;

namespace OrderManagementSystem.API.Helper
{
    public class ProfileMapping:Profile
    {
        public ProfileMapping()
        {
            CreateMap<Order, OrderDto>();
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<Order,OrderToResturnDto>().ForMember(dest=>dest.CustomerName,src=>src.MapFrom(s=>s.Customer.UserName));
            CreateMap<OrderItem,OrderItemToReturnDto>();
            CreateMap<Invoice,InvoiceDto>();
        }
    }
}
