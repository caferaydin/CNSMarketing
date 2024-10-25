using CNSMarketing.Service.Abstraction.Service.UserRole;
using MediatR;

namespace CNSMarketing.Service.Features.Command.AppUser.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, BaseCommandResponseModel>
    {
        readonly IUserService _userService;
        public AssignRoleToUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<BaseCommandResponseModel> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
        {
            await _userService.AssignRoleToUserAsnyc(request.UserId, request.Roles);

            return new()
            {
                success = true,
                message = "Complated."
            };
        }
    }
}
