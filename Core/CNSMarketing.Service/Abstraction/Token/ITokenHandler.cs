using System;
using CNSMarketing.Domain.Entity.Authentication;
using CNSMarketing.Domain.Entity.Common;
using CNSMarketing.Application.Models.Responses.Common;

namespace CNSMarketing.Application.Abstraction.Token;

public interface ITokenHandler
{
    Task<TokenResponseModel> CreateAccessToken(int second, AppUser appUser);
    string CreateRefreshToken();
    Task<TokenInfo> GetUserFromTokenAsync(string token);
}
