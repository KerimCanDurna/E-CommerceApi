using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.Services.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetList
{
    public class GetListProductQuery : IRequest<List<GetListProductListItemDto>>
    {
        public int CategoryId { get; set; }

        public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, List<GetListProductListItemDto>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public GetListProductQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }

            public async Task<List<GetListProductListItemDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
            {
                var products = await _productRepository.GetListAsync(
                    predicate: b => b.CategoryId == request.CategoryId,
                    include: c => c.Include(c => c.Category),
                    cancellationToken: cancellationToken,
                    withDeleted: true
                    );

                var response = _mapper.Map<List<GetListProductListItemDto>>(products);
                return response;

            }

          
        }

    }
}
