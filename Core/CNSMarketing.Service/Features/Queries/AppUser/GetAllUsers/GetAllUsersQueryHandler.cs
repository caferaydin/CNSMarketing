using CNSMarketing.Application.Abstraction.Service.UserRole;
using MediatR;

namespace CNSMarketing.Application.Features.Queries.AppUser.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {
        readonly IUserService _userService;

        public GetAllUsersQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllUsersAsync(request.PageIndex, request.PageSize);

            return new()
            {
                Users = users,
                TotalCount = _userService.TotalUsersCount
            };
        }
    }
}
