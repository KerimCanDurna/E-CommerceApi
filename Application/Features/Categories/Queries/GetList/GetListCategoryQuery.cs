using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Categories.Queries.GetList
{
    public class GetListCategoryQuery : IRequest<List<CategoryDto>>
    {


        public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, List<CategoryDto>>
        {
            private readonly ICategoryRepository _categoryRepository;
            private readonly IMapper _mapper;
            
            

            public GetListCategoryQueryHandler(ICategoryRepository productRepository, IMapper mapper)
            {
                _categoryRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<List<CategoryDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
            {
                
                
                var categories = await _categoryRepository.GetListAsync(                    
                   
                    include: c => c.Include(c => c.SubCategories),                    
                    orderBy: c => c.OrderBy(c => c.Name),
                    cancellationToken: cancellationToken,
                    withDeleted: true
                    );
                     var result = _mapper.Map<List<CategoryDto>>(BuildTree(categories));             
                
                return result;

            }           
                public List<Category> BuildTree(List<Category> allCategories)
                {
                    //  ParentCategoryId null olanları buluyoruz
                    var rootCategories = allCategories.Where(c => c.ParentCategoryId== null).ToList();

                    // foreach in içine hem tüm kategoriler hemde parentId sıfır olanları attım 
                    foreach (var rootCategory in rootCategories)
                    {
                        BuildSubtree(rootCategory, allCategories); // sub categorileri yerleştirmke için yaptım  her bir eleman için ağacı gezdik 
                    }

                    return rootCategories;
                }

                private void BuildSubtree(Category category, List<Category> allCategories)
                {
                    // Sub kategorileri bulmak için tüm kategoriler içinde ust kategorisi kategori Id sine esitolanları buldum 
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
