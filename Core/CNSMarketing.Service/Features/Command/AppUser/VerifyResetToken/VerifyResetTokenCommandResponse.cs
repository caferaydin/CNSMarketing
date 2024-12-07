using System.Text.Json.Serialization;

namespace CNSMarketing.Application.Features.Command.AppUser.VerifyResetToken
{
    public class VerifyResetTokenCommandResponse
    {
        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

        public bool State { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}
