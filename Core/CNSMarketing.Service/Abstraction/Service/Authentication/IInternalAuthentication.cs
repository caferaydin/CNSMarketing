using System;

namespace CNSMarketing.Service.Abstraction.Service.Authentication;

public interface IInternalAuthentication
{
    Task<Models.DTOs.Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime);
    Task<Models.DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
}
