using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Application.Abstraction.Token;
using CNSMarketing.Application.Models.Responses.Common;
using CNSMarketing.Application.Repositories.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CNSMarketing.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICustomerReadRepository _customerReadRepository;


        public TokenHandler(IConfiguration configuration, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, ICustomerReadRepository customerReadRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            _customerReadRepository = customerReadRepository;
        }


        public async Task<TokenResponseModel> CreateAccessToken(int second, AppUser user)
        {
            TokenResponseModel token = new();
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256Signature);

            token.Expiration = DateTime.UtcNow.AddDays(60).AddSeconds(second);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"], // Bu kısımın doğru olduğundan emin olun
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: new List<Claim>
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id), // Kullanıcı Id'sini ekleyin
            new Claim(ClaimTypes.Role, "Admin") // Kullanıcının rolü
                }
            );

            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);
            token.RefreshToken = CreateRefreshToken();
            return token;
        }


        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }

        public async Task<TokenInfo> GetUserFromTokenAsync(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                // Token'ı doğrulamak için gerekli parametreleri ayarlayın
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _configuration["Token:Audience"],
                    ValidIssuer = _configuration["Token:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"])),
                    ClockSkew = TimeSpan.Zero // Token süresinin bitiminde hemen geçersiz olmasını sağlamak için
                };
                Console.WriteLine(_configuration["Token:Audience"] +  "\n"  + _configuration["Token:Issuer"]);
                // Token'ı doğrula
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                // Kullanıcı adını almak için Claim üzerinden erişim sağla
                var userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Kullanıcı Id'si
                var userName = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value; // Kullanıcı adı

                // Kullanıcıyı bul
                var user = await _userManager.FindByIdAsync(userId); // Kullanıcıyı bul

                // Token'ın süresini almak için ValidTo bilgisini alalım
                if (validatedToken is JwtSecurityToken jwtToken)
                {
                    // Kullanıcının token bitiş tarihini güncelle
                    user.RefreshTokenEndDate = jwtToken.ValidTo;
                }

                var customer = _customerReadRepository.GetWhere(x=> x.UserId == user.Id).FirstOrDefault()!;

                var responseModel = new TokenInfo()
                {
                    AccessToken = token, // Geçerli token
                    CustomerId = customer.Id,
                    UserId = user!.Id,
                    UserFullName = user.NameSurname,
                    ExpireDate = user.RefreshTokenEndDate,
                    UserLoginName = user.UserName
                };

                return responseModel;
            }
            catch (Exception)
            {
                return null; // Geçersiz token
            }
        }
    }
}
