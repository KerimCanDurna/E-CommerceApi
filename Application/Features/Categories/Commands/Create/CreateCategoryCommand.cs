using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;
using Domain.Services.Repositories;
using MediatR;

namespace Application.Features.SubCategories.Commands.Create;

public class CreateCategoryCommand : IRequest<CreatedCategoryResponse>
{
    public string Name { get; set; }
    public int? ParentCategoryID { get; set; }




    public class CreateProductCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryResponse>
    {

        private readonly ICategoryRepository _productRepository;
        private readonly IMapper _mapper;
       
        public CreateProductCommandHandler(ICategoryRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
          
        }

        public async Task<CreatedCategoryResponse>? Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {

            

            var category = _mapper.Map<Category>(request);
           category.DeletedDate = null;
           category.CreatedDate = DateTime.UtcNow;

            await _productRepository.AddAsync(category);

            CreatedCategoryResponse createproductdResponse = _mapper.Map<CreatedCategoryResponse>(category);
            return createproductdResponse;
        }
    }


}

