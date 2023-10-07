using Application.Features.TokenIdentity.Dto;
using FluentValidation;

namespace Application.Features.TokenIdentity.Validation
{
    public class CreateUserDtoValidation : AbstractValidator<CreateUserDto>
    {

        public CreateUserDtoValidation()
        {

            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.UserName).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Surname).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.City).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid e-mail address ");
            RuleFor(x => x.PasswordAgain).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Password).MinimumLength(10).WithMessage("The password must be at least 10 characters long ");
            RuleFor(x => x.Password).Matches("[A-Z]").WithMessage("The password must contain at least one uppercase letter ");
            RuleFor(x => x.Password).Matches("[0-9]").WithMessage("The password must contain at least one digit.");







            // Emailin Tekillik bilgisini User Configuration nesnesinde sağladım .




        }


    }
}
