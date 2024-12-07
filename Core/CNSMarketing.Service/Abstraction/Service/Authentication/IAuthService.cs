using System;
using CNSMarketing.Application.Abstraction.ExternalService;

namespace CNSMarketing.Application.Abstraction.Service.Authentication;

public interface IAuthService : IExternalAuthentication, IInternalAuthentication
{
    Task PasswordResetAsync(string email);
    Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
}
