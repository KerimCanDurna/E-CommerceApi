using AutoMapper;
using Domain.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ParentCategories.Commands.Delete
{
    public class DeleteParentCategoryCommand : IRequest<DeleteParentCategoryResponse>
    {
        public int Id { get; set; }

        public class DeleteBrandCommandHandler : IRequestHandler<DeleteParentCategoryCommand, DeleteParentCategoryResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public DeleteBrandCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<DeleteParentCategoryResponse> Handle(DeleteParentCategoryCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);



                await _productRepository.DeleteAsync(product);

                DeleteParentCategoryResponse response = _mapper.Map<DeleteParentCategoryResponse>(product);

                return response;
            }


        }
    }
}

