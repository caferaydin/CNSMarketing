using System;
using CNSMarketing.Application.Abstraction.Service.Authentication;
using CNSMarketing.Application.Abstraction.Service.UserRole;
using MediatR;

namespace CNSMarketing.Application.Features.Command.Role.CreateRole;

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
            IsSuccess = result
        };
    }
}
