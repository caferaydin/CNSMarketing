using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNSMarketing.Service.Features.Command.AppUser.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
