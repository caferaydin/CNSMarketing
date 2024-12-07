using System;
using MediatR;

namespace CNSMarketing.Application.Features.Command.Role.CreateRole;

public class CreateRoleCommandRequest : IRequest<BaseCommandResponseModel>
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}
