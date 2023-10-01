using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.Services.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.Create;

public class CreateProductCommand : IRequest<CreatedProductResponse>
{
    public string Name { get; set; }
    public string ProductDetail { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int SubCategoryId { get; set; }




    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductResponse>
    {

        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
           
        }

        public async Task<CreatedProductResponse>? Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            

            var product = _mapper.Map<Product>(request);
            product.DeletedDate = null;
            product.CreatedDate = DateTime.UtcNow;
            await _productRepository.AddAsync(product);

            CreatedProductResponse createproductdResponse = _mapper.Map<CreatedProductResponse>(product);
            return createproductdResponse;
        }
    }


}

