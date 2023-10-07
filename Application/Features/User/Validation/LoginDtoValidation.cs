using Application.TokenService.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.TokenIdentity.Validation
{
   
    public class LoginDtoValidation : AbstractValidator<LoginDto>
    {

        public LoginDtoValidation()
        {

            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} must be mail format");
            
           






            // Emailin Tekillik bilgisini User Configuration nesnesinde sağladım .




        }


    }
}
