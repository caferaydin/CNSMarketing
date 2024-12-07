using CNSMarketing.Application.Abstraction.Service.Authentication;
using CNSMarketing.Application.Models.DTOs;
using MediatR;

namespace CNSMarketing.Application.Features.Command.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        readonly IAuthService _authService;
        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);

            if (token == null)
            {
                return new RefreshTokenLoginCommandResponse
                {
                    IsSuccess = false,
                    Message = "Invalid or expired refresh token."
                };
            }

            return new RefreshTokenLoginCommandResponse
            {
                IsSuccess = true,
                Token = token
            };
        }
    }
}
