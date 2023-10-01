using Application.TokenService.Dto;
using AutoMapper;
using Core;
using Domain.AgregateModels.UserModel;
using Domain.IServices.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Dtos;

namespace Application.Features.TokenIdentity.TokenService
{
    public class CustomAuthenticationService
    {
        private readonly Client _clients;
        private readonly TokenServices _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IUserRepository<UserRefreshToken> _userRefresTokenService;
        private readonly IMapper _mapper;

        public CustomAuthenticationService(IOptions<Client> optionsClient, TokenServices tokenService, UserManager<User> userManager, AppDbContext dbContext, IUserRepository<UserRefreshToken> userRefreshTokenService, IMapper mapper)
        {
            _clients = optionsClient.Value;

            _tokenService = tokenService;
            _userManager = userManager;
            _context = dbContext;
            _userRefresTokenService = userRefreshTokenService;
            _mapper = mapper;
        }

        public async Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) throw new Exception("Mail or Password Wrong");

            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new Exception("Password is wrong");
            }
            var token = _tokenService.CreateToken(user);

            var userRefreshToken = await _userRefresTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

            if (userRefreshToken == null)
            {
                await _userRefresTokenService.AddAsync(new UserRefreshToken { UserId = user.Id, Code = token.RefreshToken, Expiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }

            await _context.SaveChangesAsync();

            return Response<TokenDto>.Success(token, 200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            // var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

             var client = _mapper.Map<Client>(clientLoginDto);
            if (_clients.Id == clientLoginDto.Id && _clients.Secret == clientLoginDto.Secret)
            {
                var token = _tokenService.CreateTokenByClient(client);
                return Response<ClientTokenDto>.Success(token, 200);
            }
            else
                return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found", 404, true);

            //if (client == null)
            //{
            //    return Response<ClientTokenDto>.Fail("ClientId or ClientSecret not found", 404, true);
            //}



            
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefresTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();

            if (existRefreshToken == null)
            {
                return Response<TokenDto>.Fail("Refresh token not found", 404, true);
            }

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null)
            {
                return Response<TokenDto>.Fail("User Id not found", 404, true);
            }

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _context.SaveChangesAsync();

            return Response<TokenDto>.Success(tokenDto, 200);
        }

        public async Task<Response<NoContentDto>> RevokeRefreshToken(string refreshToken)
        {
            var existRefreshToken = await _userRefresTokenService.Where(x => x.Code == refreshToken).SingleOrDefaultAsync();
            if (existRefreshToken == null)
            {
                return Response<NoContentDto>.Fail("Refresh token not found", 404, true);
            }

            _userRefresTokenService.Remove(existRefreshToken);

            await _context.SaveChangesAsync();

            return Response<NoContentDto>.Success(200);
        }
    }
}
