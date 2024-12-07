using CNSMarketing.Application.Abstraction.Service.UserRole;
using CNSMarketing.Application.Models.Responses.Authentication;
using MediatR;

namespace CNSMarketing.Application.Features.Command.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, BaseCommandResponseModel>
    {
        readonly IUserService _userService;
        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseCommandResponseModel> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponseModel response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                PasswordConfirm = request.PasswordConfirm,
                Username = request.Username,
            });

            return new()
            {
                IsSuccess = response.success,
                Message = response.message,
            };

            //throw new UserCreateFailedException();
        }
    }
}
