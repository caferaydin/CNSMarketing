using System.Text.Json.Serialization;

namespace CNSMarketing.Application.Models.SocialMedia.ExternalModel.Linkedln
{
    public class LinkedlnAccessTokenRequestModel
    {
        [JsonPropertyName("grant_type")]
        public string? GrantType { get; set; }
        [JsonPropertyName("code")]
        public string? Code { get; set; }
        [JsonPropertyName("client_id")]
        public string? ClientId { get; set; }
        [JsonPropertyName("client_secret")]
        public string? ClientSecret { get; set; }
        [JsonPropertyName("redirect_uri")]
        public string? RedirectUri { get; set; }
    }
}
