using CNSMarketing.Service.Models.DTOs;

namespace CNSMarketing.Service.Features.Command.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        
    }
    public class LoginUserSuccessCommandResponse : LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}
