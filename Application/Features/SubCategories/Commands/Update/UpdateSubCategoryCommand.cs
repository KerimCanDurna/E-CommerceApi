using AutoMapper;
using Domain.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SubCategories.Commands.Update;

public class UpdateSubCategoryCommand : IRequest<UpdateSubCategoryResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentCategoryID { get; set; }

    


    public class UpdateBrandCommandHandler : IRequestHandler<UpdateSubCategoryCommand, UpdateSubCategoryResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<UpdateSubCategoryResponse> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
           

            product = _mapper.Map(request, product);
            

            await _productRepository.UpdateAsync(product);

            UpdateSubCategoryResponse response = _mapper.Map<UpdateSubCategoryResponse>(product);

            return response;
        }
    }
}

