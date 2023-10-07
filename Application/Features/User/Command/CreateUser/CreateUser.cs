using Application.Features.TokenIdentity.Dto;
using AutoMapper;
using Domain.AgregateModels.UserModel;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.TokenIdentity.Command.CreateUser
{
    public class CreateUser : IRequest<UserDto>
    {
        public string? GuestId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public string PasswordAgain { get; set; }

        public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
        {
            private readonly UserManager<User> _userManager;
            private readonly IMapper _mapper;

            public CreateUserHandler(UserManager<User> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<UserDto>? Handle(CreateUser request, CancellationToken cancellationToken)
            {
                var user = new User { Email = request.Email, UserName = request.UserName, Name = request.Name, Surname = request.Surname, City = request.City, GuestId = request.GuestId };


                if (request.Password != request.PasswordAgain)
                {
                    throw new Exception("Password  is Not Match");
                }

                var result = await _userManager.CreateAsync(user, request.Password);

                var response = _mapper.Map<UserDto>(user);

                return response;


            }
        }
    }
}


