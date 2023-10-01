using Application.TokenService.Dto;
using FluentValidation;

namespace Application.Features.TokenIdentity.Validation
{
    public class UserDtoValidation :AbstractValidator<UserDto>
    {

        public UserDtoValidation()
        {

            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.City).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid e-mail address ");

            // Emailin Tekillik bilgisini User Configuration nesnesinde sağladım .
           



        }


    }
}
