using AutoMapper;
using Domain.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Update;

public class UpdateProductCommand : IRequest<UpdateProductResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ProductDetail { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public int? CategoryId { get; set; }

   
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public UpdateBrandCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
           

            product = _mapper.Map(request, product);
            

            await _productRepository.UpdateAsync(product);

            UpdateProductResponse response = _mapper.Map<UpdateProductResponse>(product);

            return response;
        }
    }
}

