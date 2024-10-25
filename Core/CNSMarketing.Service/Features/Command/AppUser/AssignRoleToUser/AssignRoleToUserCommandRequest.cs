using MediatR;

namespace CNSMarketing.Service.Features.Command.AppUser.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest : IRequest<BaseCommandResponseModel>
    {
        public required string UserId { get; set; }
        public required string[] Roles { get; set; }
    }
}
