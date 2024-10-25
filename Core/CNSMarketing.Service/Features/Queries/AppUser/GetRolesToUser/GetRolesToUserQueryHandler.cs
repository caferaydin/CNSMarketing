using CNSMarketing.Service.Abstraction.Service.UserRole;
using MediatR;

namespace CNSMarketing.Service.Features.Queries.AppUser.GetRolesToUser
{
    public class GetRolesToUserQueryHandler : IRequestHandler<GetRolesToUserQueryRequest, GetRolesToUserQueryResponse>
    {
        readonly IUserService _userService;
        private IRoleService _roleService;

        public GetRolesToUserQueryHandler(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public async Task<GetRolesToUserQueryResponse> Handle(GetRolesToUserQueryRequest request, CancellationToken cancellationToken)
        {
            var userRoles = await _userService.GetRolesToUserAsync(request.UserId);


            return new()
            {
                UserRoles = userRoles
            };
        }
    }
}
