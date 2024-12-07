using System;
using MediatR;

namespace CNSMarketing.Application.Features.Command.Role.DeleteRole;

public class DeleteRoleCommandRequest : IRequest<BaseCommandResponseModel>
{
    public required string Id { get; set; }
}
