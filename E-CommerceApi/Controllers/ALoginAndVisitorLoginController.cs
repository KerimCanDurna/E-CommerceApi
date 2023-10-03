using Application.Features.TokenIdentity.Dto;
using Application.Features.TokenIdentity.TokenService;
using Application.TokenService.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class ALoginAndVisitorLoginController : BaseController
    {
        private readonly CustomAuthenticationService _authenticationService;

        public ALoginAndVisitorLoginController(CustomAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        //api/auth/
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateTokenAsync(loginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public IActionResult VisitorUsing(ClientLoginDto clientLoginDto)
        {
            var result = _authenticationService.CreateTokenByClient(clientLoginDto);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshToken(refreshTokenDto.Token);

            return ActionResultInstance(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)

        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.Token);

            return ActionResultInstance(result);
        }
    }
}
