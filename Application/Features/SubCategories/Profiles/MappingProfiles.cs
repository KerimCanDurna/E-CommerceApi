using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Update;
using Application.Features.SubCategories.Queries.GetbyId;
using Application.Features.SubCategories.Queries.GetList;
using AutoMapper;
using Domain.AgregateModels.CategoriModel;

namespace Application.Features.SubCategories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SubCategory, SubCreateCategoryCommand>().ReverseMap();
            CreateMap<SubCategory, SubCreatedCategoryResponse>().ReverseMap();

            CreateMap<SubCategory, UpdateSubCategoryCommand>().ReverseMap();
            CreateMap<SubCategory, UpdateSubCategoryResponse>().ReverseMap();

            CreateMap<SubCategory, DeleteSubCategoryCommand>().ReverseMap();
            CreateMap<SubCategory, DeleteSubCategoryResponse>().ReverseMap();

            CreateMap<SubCategory, GetByIdSubCategoryResponse>().ReverseMap();
            CreateMap<SubCategory, GetListSubCategoryListItemDto>().ReverseMap();
        }
    }
}
