using System;
using CNSMarketing.Service.Abstraction.ExternalService;

namespace CNSMarketing.Service.Abstraction.Service.Authentication;

public interface IAuthService : IExternalAuthentication, IInternalAuthentication
{
    Task PasswordResetAsync(string email);
    Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
}
