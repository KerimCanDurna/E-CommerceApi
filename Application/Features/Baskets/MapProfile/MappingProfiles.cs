
using Application.Features.Baskets.Commands.Create;
using Application.Features.Baskets.Commands.Delete;
using Application.Features.Baskets.Dto;
using Application.Features.Baskets.Query.BasketQuery;
using Application.TokenService.Dto;
using AutoMapper;
using Domain.AgregateModels.CartModel;
using Domain.AgregateModels.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Baskets.MapProfile
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();
           // CreateMap<List< BasketItem>,List< BasketItemDto>>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<BasketItem, CreateBasketCommand>().ReverseMap();
            CreateMap<BasketItem, DeleteBasketCommand>().ReverseMap();
            CreateMap<BasketItem, GetBasketQuery>().ReverseMap();
        }
    }
}
