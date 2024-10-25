using System;
using MediatR;

namespace CNSMarketing.Service.Features.Command.Role.UpdateRole;

public class UpdateRoleCommandRequest : IRequest<BaseCommandResponseModel>
{
    public required string Id { get; set; }
    public required string Name { get; set; }
}
