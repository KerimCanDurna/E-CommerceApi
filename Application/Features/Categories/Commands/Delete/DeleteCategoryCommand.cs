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
    public class DeleteCategoryCommand : IRequest<DeleteCategoryResponse>
    {
        public int Id { get; set; }

        public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryResponse>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public DeleteCategoryCommandHandler(IProductRepository productRepository, IMapper mapper)
            {
                _productRepository = productRepository;
                _mapper = mapper;
            }
            public async Task<DeleteCategoryResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);



                await _productRepository.DeleteAsync(product);

                DeleteCategoryResponse response = _mapper.Map<DeleteCategoryResponse>(product);

                return response;
            }


        }
    }
}

