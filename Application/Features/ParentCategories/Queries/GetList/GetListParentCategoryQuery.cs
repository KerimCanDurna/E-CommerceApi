using AutoMapper;
using Domain.IServices.IRepositories;
using Domain.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ParentCategories.Queries.GetList
{
    public class GetListParentCategoryQuery : IRequest<List<GetListParentCategoryListItemDto>>
    {


        public class GetListProductQueryHandler : IRequestHandler<GetListParentCategoryQuery, List<GetListParentCategoryListItemDto>>
        {
            private readonly IParentCategoryRepository _categoryRepository;
            
            private readonly IMapper _mapper;

            public GetListProductQueryHandler(IParentCategoryRepository productRepository, IMapper mapper)
            {
                _categoryRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<List<GetListParentCategoryListItemDto>> Handle(GetListParentCategoryQuery request, CancellationToken cancellationToken)
            {
                var products = await _categoryRepository.GetListAsync(

                     include: c => c.Include(c => c.SubCategories),

                    cancellationToken: cancellationToken,
                    withDeleted: true
                    );


                var response = _mapper.Map<List<GetListParentCategoryListItemDto>>(products);
                
                return response.OrderBy(X=>X.Name).ToList();

            }
        }
    }
}
