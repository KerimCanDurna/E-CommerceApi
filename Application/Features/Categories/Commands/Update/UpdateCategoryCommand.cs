using AutoMapper;
using Domain.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubCategories.Commands.Update;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryID { get; set; }

    


    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
           

            product = _mapper.Map(request, product);
            

            await _productRepository.UpdateAsync(product);

            UpdateCategoryResponse response = _mapper.Map<UpdateCategoryResponse>(product);

            return response;
        }
    }
}

