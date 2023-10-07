using Application.Features.Categories.Queries.GetList;
using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;
using Domain.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Application.Features.SubCategories.Queries.GetbyId
{
    public class GetByIdCategoryQuery : IRequest<List<GetByIdCategoryResponse>>
    {
        public int Id { get; set; }

        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdCategoryQuery, List<GetByIdCategoryResponse>>
        {
            private readonly IMapper _mapper;
            private readonly ICategoryRepository _categoryRepository;

            public GetByIdBrandQueryHandler(IMapper mapper, ICategoryRepository category)
            {
                _mapper = mapper;
                _categoryRepository = category;
            }

            public async Task<List<GetByIdCategoryResponse>> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
            {
                var categories = await _categoryRepository.GetListAsync(

                  include: c => c.Include(c => c.SubCategories).Include(c=>c.Products),
                  orderBy: c => c.OrderBy(c => c.Name),
                  cancellationToken: cancellationToken,
                  withDeleted: true
                  );
                var result =(BuildTree(categories,request));
                var result2 = _mapper.Map<List<GetByIdCategoryResponse>>(result);

                return result2 ;
            }

            public List<Category> BuildTree(List<Category> allCategories , GetByIdCategoryQuery request)
            {
                
                var rootCategories = allCategories.Where(c => c.ParentCategoryId == request.Id ).ToList();

              
                foreach (var rootCategory in rootCategories)
                {
                    BuildSubtree(rootCategory, allCategories); // sub categorileri yerleştirmke için yaptım  her bir eleman için ağacı gezdik 
                }

                return rootCategories;
            }

            private void BuildSubtree(Category category, List<Category> allCategories)
            {
                // Sub kategorileri bulmak için tüm kategoriler içinde ust kategorisi kategori Id sine esit olanları buldum 
                var subCategories = allCategories.Where(c => c.ParentCategoryId == category.Id).ToList();

                // Alt kategorinin alt katerisi var mı kontrol ediyoruz 
                foreach (var subCategory in subCategories)
                {
                    BuildSubtree(subCategory, allCategories);
                }


                category.SubCategories = subCategories;
                
               
               
                
            }
        }


    }
}
