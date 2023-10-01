using AutoMapper;
using Domain.Services.Repositories;
using MediatR;

namespace Application.Features.SubCategories.Queries.GetbyId
{
    public class GetByIdSubCategoryQuery : IRequest<GetByIdSubCategoryResponse>
    {
        public int Id { get; set; }

        public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdSubCategoryQuery, GetByIdSubCategoryResponse>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;

            public GetByIdBrandQueryHandler(IMapper mapper, IProductRepository brandRepository)
            {
                _mapper = mapper;
                _productRepository = brandRepository;
            }

            public async Task<GetByIdSubCategoryResponse> Handle(GetByIdSubCategoryQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, withDeleted: true, cancellationToken: cancellationToken);

                var response = _mapper.Map<GetByIdSubCategoryResponse>(product);

                return response;
            }
        }


    }
}
