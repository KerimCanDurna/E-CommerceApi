using Application.Features.ParentCategories.Commands.Create;
using Application.Features.ParentCategories.Commands.Delete;
using Application.Features.ParentCategories.Queries.GetList;
using AutoMapper;
using Domain.AgregateModels.CategoriModel;

namespace Application.Features.ParentCategories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ParentCategory, ParentCreateCategoryCommand>().ReverseMap();
            CreateMap<ParentCategory, ParentCreatedCategoryResponse>().ReverseMap();

           

            CreateMap<ParentCategory, DeleteParentCategoryCommand>().ReverseMap();
            CreateMap<ParentCategory, DeleteParentCategoryResponse>().ReverseMap();

           
            CreateMap<ParentCategory, GetListParentCategoryListItemDto>().ReverseMap();
        }
    }
}
