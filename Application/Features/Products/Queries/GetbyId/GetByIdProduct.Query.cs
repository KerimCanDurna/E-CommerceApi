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

namespace Application.Features.Products.Queries.GetbyId
{
    public class GetByIdProductQuery : IRequest<List<GetByIdProductResponse>>
    {
        public int Id { get; set; }

        public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, List<GetByIdProductResponse>>
        {
            private readonly IMapper _mapper;
            private readonly IProductRepository _productRepository;

            public GetByIdProductQueryHandler(IMapper mapper, IProductRepository brandRepository)
            {
                _mapper = mapper;
                _productRepository = brandRepository;
            }

            public async Task<List<GetByIdProductResponse>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
            {
               
                var product = await _productRepository.GetAsync(
                    
                    predicate: b => b.Id == request.Id,                   
                    withDeleted: true, cancellationToken: cancellationToken);
                
                if (product.IsActive == true) 
                {
                    var response = _mapper.Map<List<GetByIdProductResponse>>(product);

                    return response;
                }                                            

                throw new Exception("Product is Not Active Now. Stock is Empty");
            }
        }


    }
}
