using MediatR;

namespace CNSMarketing.Service.Features.Command.AppUser.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest : IRequest<RefreshTokenLoginCommandResponse>
    {
        public required string RefreshToken { get; set; }
    }
}
