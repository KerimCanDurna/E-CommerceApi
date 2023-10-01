using Application.Features.Products.Commands.Create;
using Application.TokenService.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Validation
{
    

    public class CreateProductValidation : AbstractValidator<CreatedProductResponse>
    {

        public CreateProductValidation()
        {

            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.ProductDetail).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.SubCategoryId).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Price).InclusiveBetween(1,decimal.MaxValue).WithMessage("{PropertyName} is greater than 0").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Stock).InclusiveBetween(0,int.MaxValue).WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");







        }


    }
}
