using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetbyId;
using Application.Features.Products.Queries.GetList;
using AutoMapper;
using Domain.AgregateModels.CategoriModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, CreatedProductResponse>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();

            CreateMap<Product, UpdateProductResponse>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();

            CreateMap<Product, DeleteProductResponse>().ReverseMap();
            CreateMap<Product, DeleteProductCommand>().ReverseMap();


            CreateMap<Product, GetByIdProductResponse>().ReverseMap();
            CreateMap<Product, GetListProductListItemDto>().ReverseMap();
        }
    }
}
