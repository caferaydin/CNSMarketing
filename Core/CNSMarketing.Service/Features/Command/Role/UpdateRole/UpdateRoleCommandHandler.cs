using CNSMarketing.Service.Abstraction.Service.UserRole;
using MediatR;

namespace CNSMarketing.Service.Features.Command.Role.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, BaseCommandResponseModel>
{
    private readonly IRoleService _roleService;

    public UpdateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }
    public async Task<BaseCommandResponseModel> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.UpdateRole(request.Id, request.Name);
        return new()
        {
            success = result
        };
    }
}
