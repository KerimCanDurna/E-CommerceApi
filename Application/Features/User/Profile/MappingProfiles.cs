
using Application.Features.TokenIdentity.Command.CreateUser;
using Application.Features.TokenIdentity.Dto;
using Application.TokenService.Dto;
using AutoMapper;
using Domain.AgregateModels.CartModel;
using Domain.AgregateModels.CategoriModel;
using Domain.AgregateModels.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TokenIdentity.Profiles
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Guest, GuestLoginDto>().ReverseMap();
            CreateMap<CreateUserDto, UserDto>().ReverseMap();
            CreateMap<CreateUser,UserDto>().ReverseMap();
            

        }

    }
}
