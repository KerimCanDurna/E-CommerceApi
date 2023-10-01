using Application.TokenService.Dto;
using AutoMapper;
using Domain.AgregateModels.UserModel;
using Microsoft.AspNetCore.Identity;
using Shared.Dtos;

namespace Application.Features.TokenIdentity.TokenService
{
    public class UserService 
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new User { Email = createUserDto.Email, UserName = createUserDto.UserName,Name = createUserDto.Name,Surname = createUserDto.Surname, City=createUserDto.City  };

            if (createUserDto.Password != createUserDto.PasswordAgain)
            {
                throw new Exception("Password  is Not Match");
            }

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();  //Hatanın açıklamasını alıyoruz

                return Response<UserDto>.Fail(new ErrorDto(errors, true), 400);
            }
            return Response<UserDto>.Success(_mapper.Map<UserDto>(user), 200);
        }

        public async Task<Response<UserDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserDto>.Fail("UserName not found", 404, true);
            }

            return Response<UserDto>.Success(_mapper.Map<UserDto>(user), 200);
        }


    }

}
