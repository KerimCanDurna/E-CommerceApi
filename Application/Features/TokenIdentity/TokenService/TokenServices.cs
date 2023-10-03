using Application.TokenService.Dto;
using Domain.AgregateModels.UserModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.Option;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Application.Features.TokenIdentity.TokenService
{
    public class TokenServices 
    {
        private readonly UserManager<User> _userManager;

        private readonly CustomTokenOption _tokenOption;

        public TokenServices(UserManager<User> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        private string CreateRefreshToken()

        {
            var numberByte = new byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(User user , List<String> audiences)
        {
            var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())  // token ID
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));


            return userList;
        }

        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();


            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());

            return claims;
        }

        public TokenDto CreateToken(User user)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.UtcNow,  // Tokenen alındığı saaaten önce geçersiz olsun token ömrü notbeore ile accessTokenExpiration arasında olacak 
                 claims: GetClaims(user,_tokenOption.Audience),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,  // Tokenen alındığı saaaten önce geçersiz olsun token ömrü notbeore ile accessTokenExpiration arasında olacak 
                 claims: GetClaimsByClient(client),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,

                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }
    }
}
