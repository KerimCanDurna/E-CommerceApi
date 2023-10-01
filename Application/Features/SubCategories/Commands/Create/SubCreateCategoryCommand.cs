using AutoMapper;
using Domain.AgregateModels.CategoriModel;
using Domain.IServices.IRepositories;
using Domain.Services.Repositories;
using MediatR;

namespace Application.Features.SubCategories.Commands.Create;

public class SubCreateCategoryCommand : IRequest<SubCreatedCategoryResponse>
{
    public string Name { get; set; }
    public int? ParentCategoryID { get; set; }




    public class CreateProductCommandHandler : IRequestHandler<SubCreateCategoryCommand, SubCreatedCategoryResponse>
    {

        private readonly ISubCategoryRepository _productRepository;
        private readonly IMapper _mapper;
        //private readonly ProductBusinessRules _brandBusinessRules;

        public CreateProductCommandHandler(ISubCategoryRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
           // _productBusinessRules = productBusinessRules;
        }

        public async Task<SubCreatedCategoryResponse>? Handle(SubCreateCategoryCommand request, CancellationToken cancellationToken)
        {

            //await _brandBusinessRules.BrandNameCannotBeDuplicatedWhenInserted(request.Name);

            var category = _mapper.Map<SubCategory>(request);
            //category.DeletedDate = null;
            //category.CreatedDate = DateTime.UtcNow;

            await _productRepository.AddAsync(category);

            SubCreatedCategoryResponse createproductdResponse = _mapper.Map<SubCreatedCategoryResponse>(category);
            return createproductdResponse;
        }
    }


}

