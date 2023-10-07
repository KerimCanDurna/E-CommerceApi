using Application.Features.Products.Commands.Create;
using Application.Features.TokenIdentity.Command.CreateUser;
using Application.Features.TokenIdentity.Dto;
using Application.Features.TokenIdentity.TokenService;
using Application.TokenService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> LoginUSer(LoginDto loginDto)
        {
            return ActionResultInstance(await _userService.LoginUser(loginDto));
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> LoginGuest(string guestId)
        {
            return ActionResultInstance(await _userService.LoginGuest(guestId));
        }

        //api/user
        [HttpPost ("[Action]")]
        public async Task<IActionResult> RegisterUser( CreateUser createUserDto)
        {
            var response = await Mediator.Send(createUserDto);
            return Ok(response);
        }

    }
}

