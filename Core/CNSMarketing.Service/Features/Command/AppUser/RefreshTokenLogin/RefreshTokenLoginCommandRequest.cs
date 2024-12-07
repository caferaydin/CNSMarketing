using MediatR;

namespace CNSMarketing.Application.Features.Command.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
    {
        public required string RefreshToken { get; set; }
    }
}
