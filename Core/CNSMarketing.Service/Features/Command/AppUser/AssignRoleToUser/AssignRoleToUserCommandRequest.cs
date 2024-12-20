﻿using MediatR;

namespace CNSMarketing.Application.Features.Command.AppUser.AssignRoleToUser
{
    public class AssignRoleToUserCommandRequest : IRequest<BaseCommandResponseModel>
    {
        public required string UserId { get; set; }
        public required string[] Roles { get; set; }
    }
}
