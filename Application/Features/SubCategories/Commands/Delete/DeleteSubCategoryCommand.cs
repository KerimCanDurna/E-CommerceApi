using AutoMapper;
using Domain.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubCategories.Commands.Delete
{
    public class DeleteSubCategoryCommand : IRequest<DeleteSubCategoryResponse>
    {
        public int Id { get; set; }

        public class DeleteBrandCommandHandler : IRequestHandler<DeleteSubCategoryCommand, DeleteSubCategoryResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public DeleteBrandCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<DeleteSubCategoryResponse> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);



                await _productRepository.DeleteAsync(product);

                DeleteSubCategoryResponse response = _mapper.Map<DeleteSubCategoryResponse>(product);

                return response;
            }


        }
    }
}

