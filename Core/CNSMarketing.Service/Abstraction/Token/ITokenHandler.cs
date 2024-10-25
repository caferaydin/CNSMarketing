using System;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Service.Models.Responses.Common;

namespace CNSMarketing.Service.Abstraction.Token;

public interface ITokenHandler
{
    Task<TokenResponseModel> CreateAccessToken(int second, AppUser appUser);
    string CreateRefreshToken();
    Task<TokenControl> GetUserFromTokenAsync(string token);
}
