using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.SubCategories.Queries.GetList
{
    public class GetListSubCategoryQuery : IRequest<List<GetListSubCategoryListItemDto>>
    {


        public class GetListProductQueryHandler : IRequestHandler<GetListSubCategoryQuery, List<GetListSubCategoryListItemDto>>
        {
            private readonly ISubCategoryRepository _categoryRepository;
            private readonly IMapper _mapper;

            public GetListProductQueryHandler(ISubCategoryRepository productRepository, IMapper mapper)
            {
                _categoryRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<List<GetListSubCategoryListItemDto>> Handle(GetListSubCategoryQuery request, CancellationToken cancellationToken)
            {
                var products = await _categoryRepository.GetListAsync(
                    include: c => c.Include(c => c.ParentCategory),
                    

                    cancellationToken: cancellationToken,
                    withDeleted: true
                    );
               
               


                var response = _mapper.Map<List<GetListSubCategoryListItemDto>>(products);
                return response.OrderBy(X => X.ParentCategoryName).ToList();

            }
        }
    }

    

}
