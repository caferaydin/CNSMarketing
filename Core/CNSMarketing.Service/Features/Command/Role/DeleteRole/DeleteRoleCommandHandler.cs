using System;
using CNSMarketing.Application.Abstraction.Service.Authentication;
using CNSMarketing.Application.Abstraction.Service.UserRole;
using MediatR;

namespace CNSMarketing.Application.Features.Command.Role.DeleteRole;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, BaseCommandResponseModel>
{
    private readonly IRoleService _roleService;
    public DeleteRoleCommandHandler(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task<BaseCommandResponseModel> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _roleService.DeleteRole(request.Id);
        return new()
        {
            IsSuccess = result,
        };
    }
}
