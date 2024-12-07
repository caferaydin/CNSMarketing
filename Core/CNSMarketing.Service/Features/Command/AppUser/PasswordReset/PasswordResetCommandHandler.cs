using CNSMarketing.Application.Abstraction.Service.Authentication;
using MediatR;

namespace CNSMarketing.Application.Features.Command.AppUser.PasswordReset
{
    public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, BaseCommandResponseModel>
    {
        readonly IAuthService _authService;

        public PasswordResetCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<BaseCommandResponseModel> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
        {
            await _authService.PasswordResetAsync(request.Email);

            return new()
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }
}
