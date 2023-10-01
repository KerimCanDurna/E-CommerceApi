using Application.Features.ParentCategories.Commands.Create;
using Application.TokenService.Dto;
using AutoMapper;
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
            CreateMap<Client, ClientLoginDto>().ReverseMap();
        }

    }
}
