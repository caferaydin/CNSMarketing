using System;
using System.Text.Json.Serialization;

namespace CNSMarketing.Application.Features.Command;

public class BaseCommandResponseModel
{

    [JsonPropertyName("success")]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("message")]
    public string? Message { get; set; }

}
