using MediatR;

namespace CNSMarketing.Service.Features.Command.AppUser.UpdatePassword
{
    public class UpdatePasswordCommandRequest : IRequest<BaseCommandResponseModel>
    {
        public required string UserId { get; set; }
        public required string ResetToken { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirm { get; set; }
    }
}
