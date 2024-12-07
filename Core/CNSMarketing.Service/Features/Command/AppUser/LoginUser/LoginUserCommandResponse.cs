using CNSMarketing.Application.Models.DTOs;
using System.Text.Json.Serialization;

namespace CNSMarketing.Application.Features.Command.AppUser.LoginUser
{
    public class LoginUserCommandResponse
    {
        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

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
