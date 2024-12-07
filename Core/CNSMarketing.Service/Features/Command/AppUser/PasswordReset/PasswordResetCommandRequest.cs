using MediatR;

namespace CNSMarketing.Application.Features.Command.AppUser.PasswordReset
{
    public class PasswordResetCommandRequest : IRequest<BaseCommandResponseModel>
    {
        public required string Email { get; set; }
    }
}
