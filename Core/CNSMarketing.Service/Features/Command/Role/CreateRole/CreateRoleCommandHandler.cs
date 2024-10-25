using System;
using CNSMarketing.Service.Abstraction.Service.Authentication;
using CNSMarketing.Service.Abstraction.Service.UserRole;
using MediatR;

namespace CNSMarketing.Service.Features.Command.Role.CreateRole;

public class CreateRoleCommandHandler  : IRequestHandler<CreateRoleCommandRequest, BaseCommandResponseModel>
{
    private readonly IRoleService _roleService;

    public CreateRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<BaseCommandResponseModel> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.CreateRole(request.Name, request.Description);
        return new()
        {
            success = result
        };
    }
}
