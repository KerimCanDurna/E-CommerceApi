using Application.Features.Categories.Queries.GetList;
using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Update;
using Application.Features.SubCategories.Queries.GetbyId;
using AutoMapper;
using Domain.AgregateModels.CategoriModel;

namespace Application.Features.SubCategories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreatedCategoryResponse>().ReverseMap();

            CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
            CreateMap<Category, UpdateCategoryResponse>().ReverseMap();

            CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
            CreateMap<Category, DeleteCategoryResponse>().ReverseMap();

            CreateMap<Category, GetByIdCategoryResponse>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
        }
    }
}
