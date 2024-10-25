using System;

namespace CNSMarketing.Service.Models.Responses.Common;

public class TokenResponseModel
{
    public string? AccessToken { get; set; }
    public DateTime Expiration { get; set; }
    public string? RefreshToken { get; set; }
}
