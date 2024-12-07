using MediatR;

namespace CNSMarketing.Application.Features.Command.AppUser.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
