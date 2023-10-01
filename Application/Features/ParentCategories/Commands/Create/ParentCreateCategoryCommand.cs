using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;
using Domain.Services.Repositories;
using MediatR;

namespace Application.Features.ParentCategories.Commands.Create;

public class ParentCreateCategoryCommand : IRequest<ParentCreatedCategoryResponse>
{
    public string Name { get; set; }
    




    public class CreateProductCommandHandler : IRequestHandler<ParentCreateCategoryCommand, ParentCreatedCategoryResponse>
    {

        private readonly IParentCategoryRepository _productRepository;
        private readonly IMapper _mapper;
        //private readonly ProductBusinessRules _brandBusinessRules;

        public CreateProductCommandHandler(IParentCategoryRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
           // _productBusinessRules = productBusinessRules;
        }

        public async Task<ParentCreatedCategoryResponse>? Handle(ParentCreateCategoryCommand request, CancellationToken cancellationToken)
        {

            //await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

            var category = _mapper.Map<ParentCategory>(request);
            category.DeletedDate = null;
            category.CreatedDate = DateTime.UtcNow;

            await _productRepository.AddAsync(category);

            ParentCreatedCategoryResponse createproductdResponse = _mapper.Map<ParentCreatedCategoryResponse>(category);
            return createproductdResponse;
        }
    }


}

