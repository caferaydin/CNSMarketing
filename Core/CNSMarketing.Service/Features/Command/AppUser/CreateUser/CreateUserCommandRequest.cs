using MediatR;

namespace CNSMarketing.Service.Features.Command.AppUser.CreateUser
{
    public class CreateUserCommandRequest : IRequest<BaseCommandResponseModel>
    {
        public required string NameSurname { get; set; }
        public required string Username { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PasswordConfirm { get; set; }
    }
}
